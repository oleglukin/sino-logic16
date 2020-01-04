using System;
using System.Collections.Generic;
using System.Text;

namespace API
{
    public class SignalEventAggregation
    {
        public Dictionary<string, long> Functional { get; private set; }

        public Dictionary<string, long> Failed { get; private set; }

        public SignalEventAggregation()
        {
            Functional = new Dictionary<string, long>();
            Failed = new Dictionary<string, long>();
        }

        public void IncreaseFunctional(string location, long increment)
        {
            if (Functional.TryGetValue(location, out long value))
                Functional[location] = value + increment;
            else
                Functional[location] = increment;
        }

        public void IncreaseFailed(string location, long increment)
        {
            if (Failed.TryGetValue(location, out long value))
                Failed[location] = value + increment;
            else
                Failed[location] = increment;
        }
    }
}