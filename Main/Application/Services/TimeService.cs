using task_1135.Domain.Services;

namespace task_1135.Application.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }
}
