
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationService(ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor,
        UserManager<ApplicationUser> _userManager, IMapper _mapper) : ILeaveAllocationService
    {
        public async Task AllocateLeave(string employeeId)
        {
            // get all the leave types
            var leaveTypes = await _context.LeaveTypes
                .Where(q => !q.LeaveAllocations.Any( x => x.EmployeeId == employeeId ))
                .ToListAsync();

            // get the current period based on the year
            var currentDate = DateTime.Now;
            var period = await _context.Period.SingleAsync(q => q.EndDate.Year == currentDate.Year);

            // calculate leave based on number of months left in the period
            var monthsRemaining = period.EndDate.Month - currentDate.Month;

            // foreach leave type, create an allocation entry
            foreach (var leaveType in leaveTypes)
            {
                // works but not a good practice as it will hit DB multiple times
                //var allocationExists = await AllocationExists(employeeId, period.Id, leaveType.Id);
                //if (allocationExists)
                //{
                //    continue;
                //}
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accuralRate * monthsRemaining) // type casting to int
                };

                _context.Add(leaveAllocation); // adding it into the database
            }
            await _context.SaveChangesAsync();
        }

        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            //string employeeId = string.Empty;
            //if(string.IsNullOrEmpty(userId))
            //{
            //    employeeId = userId;
            //}
            //else
            //{
            //    var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User); // using HttpContext get the userId
            //    employeeId = user?.Id;
            //}

            var currentDate = DateTime.Now;
            var leaveAllocations = await _context.LeaveAllocation // When we write this type like _context it fetches from DB
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == userId && q.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
        {
            var user = string.IsNullOrEmpty(userId) 
                ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User)
                : await _userManager.FindByIdAsync(userId); 

            var allocations = await GetAllocations(user.Id);
            var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypesCount = await _context.LeaveTypes.CountAsync();

            //var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            var employeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVmList,
                IsCompletedAllocation = leaveTypesCount == allocations.Count
            };

            return employeeVm;
        }

        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employees;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await _context.LeaveAllocation
               .Include(q => q.LeaveType)
               .Include(q => q.Employee)
               .FirstOrDefaultAsync(q => q.Id == allocationId);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

            return model;
        }

        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVm)
        {
            //var leaveAllocation = await GetEmployeeAllocation(allocationEditVm.Id);
            //if(leaveAllocation == null)
            //{
            //    throw new Exception("Leave Allocation Record Does Not Exists");
            //}
            //leaveAllocation.Days = allocationEditVm.Days;
            // option 1 _context.Update(leaveAllocation); // updates every single field
            // option 2 _context.Entry(leaveAllocation).State = EntityState.Modified;
            // await _context.SaveChangesAsync();

            await _context.LeaveAllocation
                .Where(q => q.Id == allocationEditVm.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVm.Days));
        }

        private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
        {
            var exists = await _context.LeaveAllocation.AnyAsync(q =>
                q.EmployeeId == userId
                && q.LeaveTypeId == leaveTypeId
                && q.PeriodId == periodId
            );
            return exists;
        }
    }
}
