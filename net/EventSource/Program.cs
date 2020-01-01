using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EventSource
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started application");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var worker = new Worker(configuration);
            worker.DoTheJobAsync().Wait();

            Console.WriteLine("Finished application. Press Enter...");
        }
    }
}