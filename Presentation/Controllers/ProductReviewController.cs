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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllReviews()
        {
            return Ok(await _productReviewService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById([FromRoute] string id)
        {
            return Ok(await _productReviewService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] CreateReviewDto createReviewDto)
        {
            var review = await _productReviewService.AddAsync(createReviewDto);
            return CreatedAtAction(nameof(AddReview), new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview([FromRoute] string id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            var review = await _productReviewService.UpdateAsync(id, updateReviewDto);
            return Ok(review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] string id)
        {
            var deleted = await _productReviewService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
