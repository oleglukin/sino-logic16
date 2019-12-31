using API.Models;
using com.espertech.esper.client;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;

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
            var resultSet = new Dictionary<string, (long, long)>();

            foreach(var entry in aggregation.Functional)
            {
                resultSet[entry.Key] = (entry.Value, 0);
            }

            foreach (var entry in aggregation.Failed)
            {
                if (resultSet.TryGetValue(entry.Key, out (long, long) value))
                    resultSet[entry.Key] = (value.Item1, entry.Value);
                else
                    resultSet[entry.Key] = (0, entry.Value);
            }

            var sb = new StringBuilder("[");
            foreach (var entry in resultSet)
            {
                sb.Append("{");
                sb.Append($"\"id_location\":\"{entry.Key}\",");
                sb.Append($"\"functional\":{entry.Value.Item1},");
                sb.Append($"\"failed\":{entry.Value.Item2},");
                sb.Append("},");
            }
            sb.Remove(sb.Length - 1, 1).Append("]");

            return sb.ToString();
        }
    }
}