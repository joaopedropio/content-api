using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ContentApiTests
{
    public class MovieRepositoryTests
    {
        private MovieRepository movieRepository;
        [SetUp]
        public void Setup()
        {
            var connectionString = "Server=localhost;Database=content;Uid=content;Pwd=content1234";
            this.movieRepository = new MovieRepository(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        private Movie GetSampleMovie()
        {
            var imagesPaths = new List<string>()
            {
                "/image/piratas1.png",
                "/image/piratas2.png",
                "/image/piratas3.png",
            };
            
            return new Movie()
            {
                CoverImage = new Media(),
                Images = new List<Media>(),
                Video = new Media(),
                Budget = 123000000,
                Country = "EUA",
                Duration = 123123,
                Name = "Piratas do Caribe",
                ShortDescription = "Uns piratas ai muito loucos",
                Studio = "Disney",
                Synopsis = "Jack Sparrow tava fazendo umas baguncinhas no Caribe quando apareceu uma aventura do barulho",
                ReleaseDate = DateTime.Parse("01-01-2001 00:00:00")
            };
        }

        [Test]
        public void Should_Post_Movie()
        {
            var movie = GetSampleMovie();

            var movieId = this.movieRepository.Insert(movie);

            Assert.Pass();
        }

        [Test]
        public void Should_Get_Movie()
        {
            var sampleMovie = GetSampleMovie();

            var movieId = this.movieRepository.Insert(sampleMovie);

            var persistedMovie = this.movieRepository.Get(movieId);

            Assert.AreEqual(sampleMovie.Budget, persistedMovie.Budget);
            Assert.AreEqual(sampleMovie.Country, persistedMovie.Country);
            Assert.AreEqual(sampleMovie.CoverImage, persistedMovie.CoverImage);
            Assert.AreEqual(sampleMovie.Duration, persistedMovie.Duration);
            Assert.AreEqual(sampleMovie.Images.Count, persistedMovie.Images.Count);
            Assert.AreEqual(sampleMovie.Name, persistedMovie.Name);
            Assert.AreEqual(sampleMovie.Professionals.Count, persistedMovie.Professionals.Count);
            Assert.AreEqual(sampleMovie.ReleaseDate, persistedMovie.ReleaseDate);
            Assert.AreEqual(sampleMovie.ShortDescription, persistedMovie.ShortDescription);
            Assert.AreEqual(sampleMovie.Studio, persistedMovie.Studio);
            Assert.AreEqual(sampleMovie.Synopsis, persistedMovie.Synopsis);
            Assert.AreEqual(sampleMovie.Video, persistedMovie.Video);
        }

        [Test]
        public void Should_Delete_Movie()
        {
            var sampleMovie = GetSampleMovie();

            var movieId = this.movieRepository.Insert(sampleMovie);

            this.movieRepository.Delete(movieId);

            Assert.Throws<InvalidOperationException>(() => this.movieRepository.Get(movieId), "Sequence contains no elements");
        }
    }
}