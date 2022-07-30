using Domain;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _memoryCache;
        private const string CACHE_KEY = "cacheKey";
        public DataService(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        private readonly MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(5)
        };
        public IList<Person> Fetch(bool invalidateCache = false)
        {
            IList<Person> persons = null;
            try
            {
                _memoryCache.TryGetValue(CACHE_KEY, out persons);

                if (invalidateCache || persons == null)
                {
                    var baseUri = _configuration["baseuri"];
                    var method = _configuration["method"];

                    var client = new RestClient(baseUri);
                    var request = new RestRequest(method);

                    var response = client.Execute(request);
                    var result = response.Content;
                    persons = PersonFormatHelper.GetPersons(result);
                    _memoryCache.Set(CACHE_KEY, persons, options);
                }

            }
            catch (Exception ex)
            {

            }
            return persons;
        }
    }
}
