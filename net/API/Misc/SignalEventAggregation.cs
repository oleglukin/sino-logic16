namespace API
{
    public class SignalEventAggregation
    {
        public long Functional { get; private set; }

        public long Failed { get; private set; }

        public SignalEventAggregation()
        {
            Functional = 0;
            Failed = 0;
        }

        public void IncreaseFunctional(long increment)
        {
            Functional += increment;
        }

        public void IncreaseFailed(long increment)
        {
            Failed += increment;
        }
    }
}