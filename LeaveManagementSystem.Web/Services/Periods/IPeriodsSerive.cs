namespace LeaveManagementSystem.Web.Services.Periods
{
    public interface IPeriodsSerive
    {
        Task<Period> GetCurrentPeriod();
    }
}