using LeaveManagementSystem.Web.Services.LeaveAllocations;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationService _leaveAllocationService, ILeaveTypesService _leaveTypesService) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var employees = await _leaveAllocationService.GetEmployees();
            return View(employees);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(string? id)
        {
            await _leaveAllocationService.AllocateLeave(id);
            return RedirectToAction(nameof(Details), new { userId = id });
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _leaveAllocationService.GetEmployeeAllocation(id.Value);
            if (allocation == null)
            {
                return NotFound();
            }
            return View(allocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM allocation)
        {
            // Remove validation errors for read-only nested objects not posted back from the form
            // bug fixes as it was not reloading while we were updating the leave for that employee
            foreach (var key in ModelState.Keys
                .Where(k => k.StartsWith("Period") || k.StartsWith("LeaveType.Name") || k.StartsWith("LeaveType.NumberOfDays") || k.StartsWith("Employee.FirstName") || k.StartsWith("Employee.LastName") || k.StartsWith("Employee.Email"))
                .ToList())
            {
                ModelState.Remove(key);
            }

            if (await _leaveTypesService.DaysExceedMaximum(allocation.LeaveType.Id, allocation.Days))
            {
                ModelState.AddModelError("Days", "The allocation exceeds the maximum leave type value");
            }

            if (ModelState.IsValid)
            {
                await _leaveAllocationService.EditAllocation(allocation);
                return RedirectToAction(nameof(Details), new { userId = allocation.Employee.Id });
            }

            var days = allocation.Days;
            allocation = await _leaveAllocationService.GetEmployeeAllocation(allocation.Id);
            allocation.Days = days;
            return View(allocation);
        }

        public async Task<IActionResult> Details(string? userId)
        {
            var employeeVm = await _leaveAllocationService.GetEmployeeAllocations(userId);
            return View(employeeVm);
        }
    }
}
