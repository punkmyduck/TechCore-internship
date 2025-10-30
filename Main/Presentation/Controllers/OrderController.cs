using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace task_1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;
        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] List<LineItem> items)
        {
            var command = new SubmitOrderCommand(Guid.NewGuid(), items);
            await _bus.Publish(command);
            return Accepted();
        }
    }
}
