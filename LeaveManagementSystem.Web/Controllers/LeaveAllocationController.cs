using LeaveManagementSystem.Web.Services.LeaveAllocations;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationService _leaveAllocationService) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var employees = await _leaveAllocationService.GetEmployees();
            return View(employees);
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> AllocateLeave(string? userId)
        {
            await _leaveAllocationService.AllocateLeave(userId);
            return RedirectToAction(nameof(Details), new { userId });
        }

        public async Task<IActionResult> Details(string? userId)
        {
            var employeeVm = await _leaveAllocationService.GetEmployeeAllocations(userId);
            return View(employeeVm);
        }
    }
}
