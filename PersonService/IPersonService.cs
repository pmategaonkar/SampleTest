using Domain;
using System.Collections.Generic;

namespace PersonServices
{
    public interface IPersonService
    {
        string GetFullName(int id);
        string GetFirstNamesByAge(int age);
        IList<string> GetGenderCountGroupedByAge();
    }
}
