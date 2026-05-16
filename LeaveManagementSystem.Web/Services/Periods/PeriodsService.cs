
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.Periods
{
    public class PeriodsService(ApplicationDbContext _context) : IPeriodsSerive
    {
        public async Task<Period> GetCurrentPeriod()
        {
            var currentDate = DateTime.Now;
            var period = await _context.Period.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            return period;
        }
    }
}