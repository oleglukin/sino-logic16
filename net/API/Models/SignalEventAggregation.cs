namespace API.Models
{
    public class SignalEventAggregation
    {
        private long _functional;
        private long _failed;

        public SignalEventAggregation()
        {
            _functional = 0;
            _failed = 0;
        }

        public void Increase(long functional, long failed)
        {
            _functional += functional;
            _failed += failed;
        }

        public long Functional
        {
            get => _functional;
        }

        public long Failed
        {
            get => _failed;
        }
    }
}