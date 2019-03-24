using ContentApi.Domain.Entities;

namespace ContentApi.Database
{
    public interface IContentRepository
    {
        Content Get(string name);
        void Create(Content content);
        void Remove(string name);
    }
}
