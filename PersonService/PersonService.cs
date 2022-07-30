using DataService;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonServices
{
    public class PersonService : IPersonService
    {

        private IDataService _dataService;

        public PersonService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public string GetFullName(int id)
        {
            var persons = GetAll();
            var result = persons.Where(p => p.Id == id).Select(t => t.First + " " + t.Last);
            if(result.Count() > 1)
            {
                
            }
            return string.Join(",", result.ToArray());
        }

        private IList<Person> GetAll()
        {            
            var persons = _dataService.Fetch();
            return persons;
        }

        public string GetGenderCountGroupedByAge()
        {
            var persons = _dataService.Fetch();
            throw new NotImplementedException();
        }

        public string GetFirstNamesByAge(int age)
        {
            var persons = _dataService.Fetch();
            Console.WriteLine(persons.Where(p => p.Age == age).Count());
            return  string.Join(",", persons.Where(p => p.Age == age).Select(t => t.First).ToArray());
        }

        IList<string> IPersonService.GetGenderCountGroupedByAge()
        {
            throw new NotImplementedException();
        }
    }
}
