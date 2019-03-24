using ContentApi.Domain.Entities;
using System.Collections.Generic;

namespace ContentApi.Domain.Repositories
{
    public interface IPersonRepository
    {
        IList<Person> Get();
        Person Get(uint id);
        bool Delete(uint id);
        uint Insert(Person person);
    }
}
