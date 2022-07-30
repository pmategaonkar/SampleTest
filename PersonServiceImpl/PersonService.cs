using Domain;
using Microsoft.Extensions.Configuration;
using PersonService;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration.Json;
using DataService;

namespace PersonServiceImpl
{
    public class PersonService : IPersonService
    {
        public Person Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Person> GetAll()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            IDataService srv = new DataService.DataService(configuration);
            var perspms = srv.Fetch();
        }
    }
}
