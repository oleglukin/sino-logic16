using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventSource
{
    class Worker
    {
        private static readonly HttpClient client = new HttpClient();

        private IConfigurationRoot Config { get; }
        private string ApiEndpoint;


        public Worker(IConfigurationRoot conf)
        {
            Config = conf;
            ApiEndpoint = Config.GetSection("ApiEndpoint").Value;
        }


        public async Task DoTheJobAsync()
        {
            var obj = new {
                id_sample = "76rtw",
                num_id = "ffg#er111",
	            id_location = "3211.2334",
	            id_signal_par = "0xcv11cs",
	            id_detected = "None",
	            id_class_det = "req11"
            };

            string json = JsonConvert.SerializeObject(obj);

            HttpContent loginData = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(ApiEndpoint, loginData);

            Console.WriteLine(response.StatusCode);
        }
    }
}