using System.Collections.Generic;

namespace ContentApi.Domain.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IList<T> Get();
        T Get(uint id);
        bool Delete(uint id);
        uint Insert(T media);
    }
}
