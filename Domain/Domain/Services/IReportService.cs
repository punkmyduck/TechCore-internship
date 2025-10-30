using Domain.DTOs;

namespace Domain.Services
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetBooksPerAuthorAsync();
    }
}
