using System.Collections.Generic;

namespace ContentApi.Domain.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IList<T> Get();
        T Get(uint id);
        void Delete(uint id);
        uint Insert(T media);
        IEnumerable<T> Parse(IEnumerable<dynamic> queryResult);
    }
}
