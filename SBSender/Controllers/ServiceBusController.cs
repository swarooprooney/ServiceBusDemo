using Microsoft.AspNetCore.Mvc;
using SBSender.Services;
using SBShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceBusController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public ServiceBusController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] PersonModel personModel)
        {
            await _queueService.SendMessage<PersonModel>(personModel, "personqueue");
            return Created("", null);
        }
    }
}
