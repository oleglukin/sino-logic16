using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalEventController : ControllerBase
    {
        private readonly ILogger<SignalEventController> _logger;

        public SignalEventController(ILogger<SignalEventController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public void Post([FromBody] SignalEvent signalEvent)
        {
            Console.WriteLine(signalEvent);
            //_context.TodoItems.Add(todoItem);
            //await _context.SaveChangesAsync(); // TODO do not await
        }


        [HttpGet]
        public string Get()
        {

            return "SignalEventController GET placeholda";
        }
    }
}