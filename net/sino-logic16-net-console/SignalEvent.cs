namespace sino_logic16_net_console
{
    class SignalEvent
    {
        public string Endpoint { get; set; }
        public int Id { get; set; }

        public SignalEvent(string endpoint, int id)
        {
            Endpoint = endpoint;
            Id = id;
        }
    }
}
