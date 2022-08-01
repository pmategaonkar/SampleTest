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
            .AddLogging(config => config.AddConsole())
            .BuildServiceProvider();
        
            var personService = serviceProvider.GetService<IPersonService>();            

            Console.WriteLine("The users full name for id=42");

            //TODO:Remove this line if we need to get input from user.
            //var id = Console.ReadLine();
            var id = "42";
            if(int.TryParse(id, out int checkedId))
            {
                Console.WriteLine(personService.GetFullName(checkedId));
            }
            else
            {
                Console.WriteLine("Please enter correct id");
            }

            Console.WriteLine();
            Console.WriteLine("All the users first names who are 23");
            //TODO: Remove this line if we need to get input from user.
            //var age = Console.ReadLine();
            var age = "23";

            if (int.TryParse(age, out int checkedAge))
            {
                Console.WriteLine(personService.GetFirstNamesByAge(checkedAge));
            }
            else
            {
                Console.WriteLine("Please enter correct id");
            }

            Console.WriteLine();
            Console.WriteLine("Gender count, grouped by age");
            var results = personService.GetGenderCountGroupedByAge();
            foreach(var result in results)
            {
                Console.WriteLine(result);
            }


            Console.ReadLine();
        }
    }
}
