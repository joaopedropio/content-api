using ContentClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContentClient
{
    public interface IContentClient
    {
        // Media
        IList<Media> GetMedia();
        Media GetMedia(uint id);
        bool DeleteMedia(uint id);
        uint InsertMedia(Media media);

        // Media
        IList<Person> GetPerson();
        Person GetPerson(uint id);
        bool DeletePerson(uint id);
        uint InsertPerson(Person person);

        // Media
        IList<Movie> GetMovie();
        Movie GetMovie(uint id);
        bool DeleteMovie(uint id);
        uint InsertMovie(Movie movie);
    }
}
