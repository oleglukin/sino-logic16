using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using com.espertech.esper.client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static readonly SignalEventAggregation aggregation = new SignalEventAggregation();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var engine = EPServiceProviderManager.GetDefaultProvider();
            engine.EPAdministrator.Configuration.AddEventType<SignalEvent>();

            string epl = "select id_detected, count(*) from SignalEvent#time_batch(1 sec) group by id_detected";
            EPStatement statement = engine.EPAdministrator.CreateEPL(epl);
            statement.Events += SignalUpdateEventHandler;

            services.AddSingleton(engine);
            services.AddControllers();
        }


        private static void SignalUpdateEventHandler(object sender, UpdateEventArgs e)
        {
            var attributes = (e.NewEvents.FirstOrDefault().Underlying as Dictionary<string, object>);
            Console.WriteLine("An Event (" + e.Statement.Name + ") occured:");
            foreach (var att in attributes)
            {
                Console.WriteLine($"\t{att.Key}: {att.Value}");
            }

            string id_detected = "None"; long count = 0;

            if (string.Equals(id_detected, "None", StringComparison.OrdinalIgnoreCase))
                aggregation.IncreaseFunctional(count);
            else if (string.Equals(id_detected, "Nan", StringComparison.OrdinalIgnoreCase))
                aggregation.IncreaseFailed(count);
            else
                throw new ArgumentException($"Unknown id_detected: {id_detected}");
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}