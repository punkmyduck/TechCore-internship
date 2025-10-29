using Microsoft.AspNetCore.Mvc;
using task_1135.Domain.Services;

namespace task_1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Получить отчет со всеми авторами и количеством их книг
        /// </summary>
        /// <returns>Список отчетов с именем автором и количеством его книг</returns>
        [HttpGet]
        public async Task<IActionResult> GetBooksPerAuthor()
        {
            var reports = await _reportService.GetBooksPerAuthorAsync();
            return Ok(reports);
        }
    }
}
