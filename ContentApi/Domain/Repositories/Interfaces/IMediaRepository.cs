using ContentApi.Domain.Entities;
using System.Collections.Generic;

namespace ContentApi.Domain.Repositories.Interfaces
{
    public interface IMediaRepository
    {
        IList<Media> Get();
        Media Get(int id);
        bool Delete(int id);
        int Insert(Media media);
    }
}
