using API.Models;
using com.espertech.esper.client;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalEventController : ControllerBase
    {
        private readonly EPServiceProvider engine;
        private readonly SignalEventAggregation aggregation;

        public SignalEventController(EPServiceProvider _engine, SignalEventAggregation _agg)
        {
            engine = _engine;
            aggregation = _agg;
        }


        [HttpPost]
        public void Post([FromBody] SignalEvent signalEvent)
        {
            engine.EPRuntime.SendEvent(signalEvent);
        }


        [HttpGet]
        public string Get()
        {
            return "{" +
                $"\"functional\":{aggregation.Functional}," +
                $"\"failed\":{aggregation.Failed}" +
                "}";
        }
    }
}