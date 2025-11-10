using Domain.Services;

namespace BookService.Application.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }
}
