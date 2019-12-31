using API.Models;
using com.espertech.esper.client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

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

            string epl = "select id_location, id_detected, count(*) from SignalEvent#time_batch(1 sec) group by id_location, id_detected";
            EPStatement statement = engine.EPAdministrator.CreateEPL(epl);
            statement.Events += SignalUpdateEventHandler;

            services.AddSingleton(engine);
            services.AddSingleton(aggregation);

            services.AddControllers();
        }


        private static void SignalUpdateEventHandler(object sender, UpdateEventArgs e)
        {
            var attributes = (e.NewEvents.FirstOrDefault().Underlying as Dictionary<string, object>);

            string id_detected = string.Empty, id_location = string.Empty;
            long count = 0;

            if (attributes.TryGetValue("id_location", out object val))
                id_location = val.ToString();

            if (attributes.TryGetValue("id_detected", out val))
                id_detected = val.ToString();

            if (attributes.TryGetValue("count(*)", out val))
                count = (long)val;

            if (string.Equals(id_detected, "None", StringComparison.OrdinalIgnoreCase))
                aggregation.IncreaseFunctional(id_location, count);
            else if (string.Equals(id_detected, "Nan", StringComparison.OrdinalIgnoreCase))
                aggregation.IncreaseFailed(id_location, count);
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