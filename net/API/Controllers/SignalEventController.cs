using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using com.espertech.esper.client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalEventController : ControllerBase
    {
        private readonly EPServiceProvider engine;

        public SignalEventController(EPServiceProvider _engine)
        {
            engine = _engine;
        }


        [HttpPost]
        public void Post([FromBody] SignalEvent signalEvent)
        {
            engine.EPRuntime.SendEvent(signalEvent);
        }


        [HttpGet]
        public string Get()
        {
            return "SignalEventController GET placeholda";
        }
    }
}