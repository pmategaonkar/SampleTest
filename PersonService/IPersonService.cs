using Domain;
using System.Collections.Generic;

namespace PersonService
{
    public interface IPersonService
    {
        Person Get(int id);
        IList<Person> GetAll();
    }
}
