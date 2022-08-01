using DataService;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonServices
{
    public class PersonService : IPersonService
    {

        private IDataService _dataService;
        private const string OUT_FORMAT = "Age : {0} Female: {1} Male: {2}";
        private ILogger _logger;

        public PersonService(IDataService dataService, ILogger<PersonService> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        public string GetFullName(int id)
        {
            try
            {
                var persons = GetAll();
                var result = persons.Where(p => p.Id == id).Select(t => t.First + " " + t.Last).ToArray();
                return result.Length == 0 ? "Could not find any person with id " + id : string.Join(",", result);
            }
            catch (Exception ex)
            {

                _logger.LogError("Error has occurred while getting full name of customers", ex);
                return "Some error has occurred, please refer logs for more details";
            }


        }

        private IList<Person> GetAll()
        {
            var persons = _dataService.Fetch();
            return persons;
        }

        public string GetFirstNamesByAge(int age)
        {
            try
            {
                var persons = _dataService.Fetch();

                var result = string.Join(",", persons.Where(p => p.Age == age).Select(t => t.First).ToArray());

                return result == string.Empty ? "Could not find any person of age  " + age : string.Join(",", result);

            }
            catch (Exception ex)
            {

                _logger.LogError("Error has occurred while getting customers", ex);
                return "Some error has occurred, please refer logs for more details";
            }
        }

        public IList<string> GetGenderCountGroupedByAge()
        {
            List<string> genderCountgroupedByAge = new List<string>();
            var persons = _dataService.Fetch();

            if (persons == null)
            {
                return new List<string>() { "Unable to fetch the data " };
            }

            var groupedByAge = persons.GroupBy(p => p.Age).OrderBy(p => p.Key);

            foreach (var group in groupedByAge)
            {
                var maleCount = group.ToList().Where(p => p.Gender == "M").Count();
                var femaleCount = group.ToList().Where(p => p.Gender == "F").Count();
                genderCountgroupedByAge.Add(string.Format(OUT_FORMAT, group.Key, femaleCount, maleCount));
            }

            return genderCountgroupedByAge;
        }
    }
}