using ContentApi.Database;
using ContentApi.Domain;
using NUnit.Framework;
using System;
using System.IO;

namespace ContentApiTests
{
    public class MovieRepositoryTests
    {
        private MovieRepository movieRepository;
        [SetUp]
        public void Setup()
        {
            this.movieRepository = new MovieRepository("Server=socialmovie.minivps.info;Database=content;Uid=content;Pwd=content1234");
        }

        private Movie GetSampleMovie()
        {
            return new Movie()
            {
                CoverImagePath = "/image/piratas.png",
                Budget = 123000000,
                Country = "EUA",
                Duration = 123123,
                Name = "Piratas do Caribe",
                ShortDescription = "Uns piratas ai muito loucos",
                Studio = "Disney",
                Synopsis = "Jack Sparrow tava fazendo umas baguncinhas no Caribe quando apareceu uma aventura do barulho",
                ReleaseDate = "01-01-2001"
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

            Assert.AreEqual(sampleMovie.Name, persistedMovie.Name);
            Assert.AreEqual(sampleMovie.ReleaseDate, persistedMovie.ReleaseDate);
            Assert.AreEqual(sampleMovie.ShortDescription, persistedMovie.ShortDescription);
            Assert.AreEqual(sampleMovie.Studio, persistedMovie.Studio);
            Assert.AreEqual(sampleMovie.Synopsis, persistedMovie.Synopsis);
            Assert.AreEqual(sampleMovie.Duration, persistedMovie.Duration);
            Assert.AreEqual(sampleMovie.CoverImagePath, persistedMovie.CoverImagePath);
            Assert.AreEqual(sampleMovie.Country, persistedMovie.Country);
            Assert.AreEqual(sampleMovie.Budget, persistedMovie.Budget);
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