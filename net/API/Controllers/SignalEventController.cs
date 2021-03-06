﻿using API.Models;
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
        private readonly EPRuntime epRuntime;
        private readonly SignalEventAggregation aggregation;

        public SignalEventController(EPRuntime _epRuntime, SignalEventAggregation _agg)
        {
            epRuntime = _epRuntime;
            aggregation = _agg;
        }


        [HttpPost]
        public void Post([FromBody] SignalEvent signalEvent)
        {
            epRuntime.SendEvent(signalEvent);
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

            var sb = new StringBuilder();

            if (resultSet.Count != 0)
            {
                sb.Append("[");
                foreach (var entry in resultSet)
                {
                    sb.Append("{");
                    sb.Append($"\"id_location\":\"{entry.Key}\",");
                    sb.Append($"\"functional\":{entry.Value.Item1},");
                    sb.Append($"\"failed\":{entry.Value.Item2}");
                    sb.Append("},");
                }
                sb.Remove(sb.Length - 1, 1).Append("]");
            }
            else
            {
                sb.Append("[]");
            }

            return sb.ToString();
        }


        [HttpGet("{id_location}")]
        public string GetByLocation(string id_location)
        {
            if (aggregation.Functional.TryGetValue(id_location, out long functional)
                |
                aggregation.Failed.TryGetValue(id_location, out long failed)
                )
            {
                return "{" +
                    $"\"functional\":\"{functional}\"," +
                    $"\"failed\":\"{failed}\"" +
                    "}";
            }
            else
                return $"location \"{id_location}\" not found";
        }


        [HttpDelete]
        public void ResetAggregation()
        {
            aggregation.Functional.Clear();
            aggregation.Failed.Clear();
        }


        [HttpDelete("{id_location}")]
        public string ResetAggregationByLocation(string id_location)
        {

            if (aggregation.Functional.Remove(id_location)
                |
                aggregation.Failed.Remove(id_location)
                )
                return $"Reset aggregations for location \"{id_location}\"";
            else
                return $"Location \"{id_location}\" not found";
        }
    }
}