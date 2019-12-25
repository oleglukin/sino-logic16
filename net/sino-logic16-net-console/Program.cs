using com.espertech.esper.client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sino_logic16_net_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            EPServiceProvider engine = EPServiceProviderManager.GetDefaultProvider();
            engine.EPAdministrator.Configuration.AddEventType<SignalEvent>();

            string epl = "select Endpoint, Id from SignalEvent";
            EPStatement statement = engine.EPAdministrator.CreateEPL(epl);

            statement.Events += DefaultUpdateEventHandler;

            engine.EPRuntime.SendEvent(new SignalEvent("Signal 42", 21));

            Console.WriteLine("Finished. Press Enter...");
            Console.ReadLine();
        }

        private static void DefaultUpdateEventHandler(object sender, UpdateEventArgs e)
        {
            var attributes = (e.NewEvents.FirstOrDefault().Underlying as Dictionary<string, object>);
            Console.WriteLine("An Event (" + e.Statement.Name + ") occured:");
            foreach(var att in attributes)
            {
                Console.WriteLine($"\t{att.Key}: {att.Value}");
            }
        }
    }
}