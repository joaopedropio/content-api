using ContentApi.Domain.Entities;
using System.Collections.Generic;

namespace ContentApi.Domain.Repositories.Interfaces
{
    public interface IMediaRepository
    {
        IList<Media> Get();
        Media Get(uint id);
        bool Delete(uint id);
        uint Insert(Media media);
    }
}
