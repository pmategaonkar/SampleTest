
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonServices;
using System;

namespace PersonDetailsDisplay
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            IConfiguration Configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
             .AddSingleton<IConfiguration>(provider => Configuration)
            .AddScoped<DataService.IDataService, DataService.DataService>()
            .AddScoped<IPersonService, PersonService>()
            .AddMemoryCache()
            .BuildServiceProvider();
        
            var personService = serviceProvider.GetService<IPersonService>();

            Console.WriteLine("Id of customer to get full name");
            var id = Console.ReadLine();
            if(int.TryParse(id, out int checkedId))
            {
                Console.WriteLine(personService.GetFullName(checkedId));
            }
            else
            {
                Console.WriteLine("Please enter correct id");
            }

            Console.WriteLine("Age to get customers first names");
            var age = Console.ReadLine();
            if (int.TryParse(age, out int checkedAge))
            {
                Console.WriteLine(personService.GetFirstNamesByAge(checkedAge));
            }
            else
            {
                Console.WriteLine("Please enter correct id");
            }


            //var data = personService.GetAll();
            //foreach (Person person in data)
            //{
            //    Console.WriteLine(person.Id);
            //    Console.WriteLine(person.First + " " + person.Last);
            //    Console.WriteLine(person.Age);
            //    Console.WriteLine(person.Gender);
            //}

            Console.ReadLine();
        }
    }
}
