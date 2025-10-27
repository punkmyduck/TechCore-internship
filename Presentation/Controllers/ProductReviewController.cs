using Microsoft.AspNetCore.Mvc;
using task_1135.Application.DTOs;
using task_1135.Domain.Services;

namespace task_1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewService _productReviewService;
        public ProductReviewController(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
        }

        /// <summary>
        /// Получить все отзывы
        /// </summary>
        /// <returns>Список отзывов</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllReviews()
        {
            return Ok(await _productReviewService.GetAllAsync());
        }

        /// <summary>
        /// Получить отзыв по ID
        /// </summary>
        /// <param name="id">Идентификатор отзыва</param>
        /// <returns>Отзыв с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById([FromRoute] string id)
        {
            return Ok(await _productReviewService.GetByIdAsync(id));
        }

        /// <summary>
        /// Добавить новый отзыв
        /// </summary>
        /// <param name="createReviewDto">Класс с атрибутами отзыва</param>
        /// <returns>Информация о добавленном отзыве</returns>
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] CreateReviewDto createReviewDto)
        {
            var review = await _productReviewService.AddAsync(createReviewDto);
            return CreatedAtAction(nameof(AddReview), new { id = review.Id }, review);
        }

        /// <summary>
        /// Обновить существующий отзыв
        /// </summary>
        /// <param name="id">Идентификатор отзыва</param>
        /// <param name="updateReviewDto">Класс с атрибутами для изменения отзыва</param>
        /// <returns>Информация об измененном отзыве</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview([FromRoute] string id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            var review = await _productReviewService.UpdateAsync(id, updateReviewDto);
            return Ok(review);
        }

        /// <summary>
        /// Удалить отзыв по ID
        /// </summary>
        /// <param name="id">Идентификатор отзыва</param>
        /// <returns>NotFound, если отзыва с указанным ID не существует или NoContent в случае успеха</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] string id)
        {
            var deleted = await _productReviewService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
