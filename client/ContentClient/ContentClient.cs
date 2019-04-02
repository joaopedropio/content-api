using System;
using System.Collections.Generic;
using ContentClient.Models;

namespace ContentClient
{
    public class ContentClient : IContentClient
    {
        public bool DeleteMedia(uint id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMovie(uint id)
        {
            throw new NotImplementedException();
        }

        public bool DeletePerson(uint id)
        {
            throw new NotImplementedException();
        }

        public IList<Media> GetMedia()
        {
            throw new NotImplementedException();
        }

        public Media GetMedia(uint id)
        {
            throw new NotImplementedException();
        }

        public IList<Movie> GetMovie()
        {
            throw new NotImplementedException();
        }

        public Movie GetMovie(uint id)
        {
            throw new NotImplementedException();
        }

        public IList<Person> GetPerson()
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(uint id)
        {
            throw new NotImplementedException();
        }

        public uint InsertMedia(Media media)
        {
            throw new NotImplementedException();
        }

        public uint InsertMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public uint InsertPerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
