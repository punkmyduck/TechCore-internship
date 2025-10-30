using Domain.Services;

namespace task1135.Application.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }
}
