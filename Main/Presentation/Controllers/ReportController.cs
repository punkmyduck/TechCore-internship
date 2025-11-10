using Microsoft.AspNetCore.Mvc;
using Domain.Services;

namespace task1135.Presentation.Controllers
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
        /// Отчет формируется прямым SQL-запросом при помощи Dapper
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
