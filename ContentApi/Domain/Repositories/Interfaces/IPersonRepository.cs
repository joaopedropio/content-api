using ContentApi.Domain.Entities;
using System.Collections.Generic;

namespace ContentApi.Domain.Repositories
{
    public interface IPersonRepository
    {
        IList<Person> Get();
        Person Get(int id);
        bool Delete(int id);
        int Insert(Person person);
    }
}
