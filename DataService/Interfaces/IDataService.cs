using Domain;
using System.Collections.Generic;

namespace DataService
{
    public interface IDataService
    {
        IList<Person> Fetch(bool invalidateCache = false);
    }
}
