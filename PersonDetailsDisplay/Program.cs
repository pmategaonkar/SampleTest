using DataService;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.Configuration.Json;
namespace PersonDetailsDisplay
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            IDataService srv = new DataService.DataService(configuration);
            var perspms = srv.Fetch();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
