using Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DataService
{
    public class DataService : IDataService
    {
        private IConfiguration _configuration;
        public DataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IList<Person> Fetch(bool invalidateCache = false)
        {

            try
            {
                var baseUri = _configuration["baseuri"];
                var method = _configuration["method"];

                var client = new RestClient(baseUri);
                var request = new RestRequest(method);


                var response = client.Execute(request);
                var result = response.Content;
                return PersonFormatHelper.GetPersons(result);
            }
            catch (JsonSerializationException ex)
            {

            }
            return new List<Person>();
        }
    }
}
