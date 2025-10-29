using task_1135.Application.DTOs;

namespace task_1135.Domain.Services
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetBooksPerAuthorAsync();
    }
}
