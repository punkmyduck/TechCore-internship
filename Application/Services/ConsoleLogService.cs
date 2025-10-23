using task_1135.Domain.Services;

namespace task_1135.Application.Services
{
    public class ConsoleLogService : ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
