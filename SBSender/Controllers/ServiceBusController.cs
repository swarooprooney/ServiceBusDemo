using Microsoft.AspNetCore.Mvc;
using ServiceBusHelper.Enums;
using ServiceBusHelper.Sender.ExternalServices;
using ServiceBusShared.Models;
using System.Threading.Tasks;

namespace SBSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceBusController : ControllerBase
    {
        private readonly ISenderService _queueService;

        public ServiceBusController(ISenderService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] PersonModel personModel)
        {
            await _queueService.SendMessageAsync<PersonModel>(personModel, "personqueue",ServiceBusType.Queue);
            return Created("", null);
        }

        [HttpPost("PostOrder")]
        public async Task<IActionResult> PostMessageToTopic([FromBody] string orderName)
        {
            var order = new Order { OrderId = new System.Guid(), OrderName = orderName };
            await _queueService.SendMessageAsync<Order>(order, "ecommerce", ServiceBusType.Topic);
            return Created("", null);
        }

        [HttpPost("PostPayment")]
        public async Task<IActionResult> PostMessageToTopic([FromBody] Payment payment)
        {
            await _queueService.SendMessageAsync<Payment>(payment, "ecommerce", ServiceBusType.Topic);
            return Created("", null);
        }
    }
}
