using Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DataService
{
    public class DataService : IDataService
    {
        private IConfiguration _configuration;
        private IMemoryCache _memoryCache;
        private ILogger _logger;
        private const string CACHE_KEY = "cacheKey";
        public DataService(IConfiguration configuration, IMemoryCache memoryCache, ILogger<DataService> logger)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _logger = logger;
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
                    request.Timeout = 5000; //Allow extra time to handle slowness of api
                    var response = client.Execute(request);
                    var result = response.Content;
                    persons = PersonFormatHelper.GetPersons(result, _logger);
                    _memoryCache.Set(CACHE_KEY, persons, options);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Some error has occurred while fetching the data");
            }
            return persons;
        }
    }
}
