
using LeaveManagementSystem.Web.Models.LeaveAllocations;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public interface ILeaveAllocationService
    {
        Task AllocateLeave(string employeeId);
        Task<List<EmployeeListVM>> GetEmployees();
        Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId);
    }
}
