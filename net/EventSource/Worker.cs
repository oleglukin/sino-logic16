using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventSource
{
    class Worker
    {
        private static readonly HttpClient client = new HttpClient();

        private IConfigurationRoot Config { get; }
        private string ApiEndpoint { get; }
        private int EventsToSend { get; }
        private string[] locations { get; }


        public Worker(IConfigurationRoot conf)
        {
            Config = conf;
            ApiEndpoint = Config.GetSection("ApiEndpoint").Value;

            if (int.TryParse(Config.GetSection("eventsToSend").Value, out int eventsToSend))
            {
                EventsToSend = eventsToSend;
            }

            string locationsString = Config.GetSection("locations").Value;
            locations = locationsString.Split(',').Select(x => x.Trim()).ToArray();
        }


        public async Task DoTheJobAsync()
        {
            Random rnd = new Random();
            int i = 0;
            for (; i < EventsToSend; i++)
            {
                int locationIndex = rnd.Next(0, locations.Length);  // creates a number between 1 and 12
                int statusIndex = rnd.Next(0, 2);
                string status = (statusIndex == 0) ? "None" : "Nan";
                var obj = new
                {
                    id_sample = "95ggm",
                    num_id = "fcc#wr995",
                    id_location = locations[locationIndex],
                    id_signal_par = "0xvckkep",
                    id_detected = status,
                    id_class_det = "req44"
                };

                string json = JsonConvert.SerializeObject(obj);

                HttpContent loginData = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(ApiEndpoint, loginData);
                Console.WriteLine(response.StatusCode);
            }

            Console.WriteLine($"Posted {i} events");
        }
    }
}