using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace task1135.Presentation.Controllers
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

        /// <summary>
        /// Отсылает "псевдо"событие в шину сообщений rabbitmq для обработки заказа
        /// </summary>
        /// <param name="items">Список элементов заказов (необязательно)</param>
        /// <returns>Accepted в случае успеха</returns>
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] List<LineItem> items)
        {
            var command = new SubmitOrderCommand(Guid.NewGuid(), items);
            await _bus.Publish(command);
            return Accepted();
        }
    }
}
