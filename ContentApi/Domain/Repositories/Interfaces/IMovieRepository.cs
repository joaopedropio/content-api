using ContentApi.Domain.Entities;
using System.Collections.Generic;

namespace ContentApi.Domain.Repositories
{
    public interface IMovieRepository
    {
        IList<Movie> Get();
        Movie Get(int id);
        bool Delete(int id);
        int Insert(Movie movie);
    }
}
