using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApiIntegrationTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentApiTests.RepositoryTests
{
    public class MovieRepositoryTests
    {
        private MovieRepository movieRepository;
        private MediaRepository mediaRepository;
        private PersonRepository personRepository;
        private DataHelper dataHelper;

        [SetUp]
        public void Setup()
        {
            var connectionString = Configurations.GetConnectionString();
            this.movieRepository = new MovieRepository(connectionString);
            this.mediaRepository = new MediaRepository(connectionString);
            this.personRepository = new PersonRepository(connectionString);
            this.dataHelper = new DataHelper(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        [Test]
        public void Get_Movies()
        {
            dataHelper.DeleteAll<Movie>(this.movieRepository);

            var movie = dataHelper.GetSampleMovie();

            var insertsCount = 5;

            for (int i = 0; i < insertsCount; i++)
            {
                this.movieRepository.Insert(movie);
            }

            var movies = this.movieRepository.Get();

            Assert.AreEqual(movies.Count, insertsCount);
        }

        [Test]
        public void Post_Movie()
        {
            var movie = dataHelper.GetSampleMovie();

            var movieId = this.movieRepository.Insert(movie);

            Assert.Pass();
        }

        [Test]
        public void Get_Movie()
        {
            var sampleMovie = dataHelper.GetSampleMovie();

            var movieId = this.movieRepository.Insert(sampleMovie);

            var persistedMovie = this.movieRepository.Get(movieId);

            Assert.AreEqual(sampleMovie.Budget, persistedMovie.Budget);
            Assert.AreEqual(sampleMovie.Country, persistedMovie.Country);
            Assert.AreEqual(sampleMovie.Duration, persistedMovie.Duration);
            Assert.AreEqual(sampleMovie.Name, persistedMovie.Name);
            Assert.AreEqual(sampleMovie.ReleaseDate, persistedMovie.ReleaseDate);
            Assert.AreEqual(sampleMovie.ShortDescription, persistedMovie.ShortDescription);
            Assert.AreEqual(sampleMovie.Studio, persistedMovie.Studio);
            Assert.AreEqual(sampleMovie.Synopsis, persistedMovie.Synopsis);

            // Assert Video Media
            var sampleVideo = dataHelper.GetSampleVideo();
            Assert.AreEqual(sampleVideo.Description, persistedMovie.Video.Description);
            Assert.AreEqual(sampleVideo.Name, persistedMovie.Video.Name);
            Assert.AreEqual(sampleVideo.Path, persistedMovie.Video.Path);
            Assert.AreEqual(sampleVideo.Type, persistedMovie.Video.Type);

            // Assert Cover Image Media
            var sampleCoverImage = dataHelper.GetSampleCoverImage();
            Assert.AreEqual(sampleCoverImage.Description, persistedMovie.CoverImage.Description);
            Assert.AreEqual(sampleCoverImage.Name, persistedMovie.CoverImage.Name);
            Assert.AreEqual(sampleCoverImage.Path, persistedMovie.CoverImage.Path);
            Assert.AreEqual(sampleCoverImage.Type, persistedMovie.CoverImage.Type);

            // Assert Professionals
            var sampleProfessionals = dataHelper.GetSampleProfessionals();
            Assert.AreEqual(sampleProfessionals[0].Person.Age, persistedMovie.Professionals[0].Person.Age);
            Assert.AreEqual(sampleProfessionals[0].Person.Birthday, persistedMovie.Professionals[0].Person.Birthday);
            Assert.AreEqual(sampleProfessionals[0].Person.Name, persistedMovie.Professionals[0].Person.Name);
            Assert.AreEqual(sampleProfessionals[0].Person.Nationality, persistedMovie.Professionals[0].Person.Nationality);
            Assert.AreEqual(sampleProfessionals[0].Ocupation, persistedMovie.Professionals[0].Ocupation);

            Assert.AreEqual(sampleProfessionals[1].Person.Age, persistedMovie.Professionals[1].Person.Age);
            Assert.AreEqual(sampleProfessionals[1].Person.Birthday, persistedMovie.Professionals[1].Person.Birthday);
            Assert.AreEqual(sampleProfessionals[1].Person.Name, persistedMovie.Professionals[1].Person.Name);
            Assert.AreEqual(sampleProfessionals[1].Person.Nationality, persistedMovie.Professionals[1].Person.Nationality);
            Assert.AreEqual(sampleProfessionals[1].Ocupation, persistedMovie.Professionals[1].Ocupation);
        }

        [Test]
        public void Get_MovieByName()
        {
            var sampleMovie = dataHelper.GetSampleMovie();

            this.movieRepository.Insert(sampleMovie);

            var movies = this.movieRepository.GetByName(sampleMovie.Name);
            var persistedMovie = movies.FirstOrDefault();

            Assert.GreaterOrEqual(movies.Count, 1);
            Assert.AreEqual(sampleMovie.Budget, persistedMovie.Budget);
            Assert.AreEqual(sampleMovie.Country, persistedMovie.Country);
            Assert.AreEqual(sampleMovie.Duration, persistedMovie.Duration);
            Assert.AreEqual(sampleMovie.Name, persistedMovie.Name);
            Assert.AreEqual(sampleMovie.ReleaseDate, persistedMovie.ReleaseDate);
            Assert.AreEqual(sampleMovie.ShortDescription, persistedMovie.ShortDescription);
            Assert.AreEqual(sampleMovie.Studio, persistedMovie.Studio);
            Assert.AreEqual(sampleMovie.Synopsis, persistedMovie.Synopsis);

            // Assert Video Media
            var sampleVideo = dataHelper.GetSampleVideo();
            Assert.AreEqual(sampleVideo.Description, persistedMovie.Video.Description);
            Assert.AreEqual(sampleVideo.Name, persistedMovie.Video.Name);
            Assert.AreEqual(sampleVideo.Path, persistedMovie.Video.Path);
            Assert.AreEqual(sampleVideo.Type, persistedMovie.Video.Type);

            // Assert Cover Image Media
            var sampleCoverImage = dataHelper.GetSampleCoverImage();
            Assert.AreEqual(sampleCoverImage.Description, persistedMovie.CoverImage.Description);
            Assert.AreEqual(sampleCoverImage.Name, persistedMovie.CoverImage.Name);
            Assert.AreEqual(sampleCoverImage.Path, persistedMovie.CoverImage.Path);
            Assert.AreEqual(sampleCoverImage.Type, persistedMovie.CoverImage.Type);

            // Assert Professionals
            var sampleProfessionals = dataHelper.GetSampleProfessionals();
            Assert.AreEqual(sampleProfessionals[0].Person.Age, persistedMovie.Professionals[0].Person.Age);
            Assert.AreEqual(sampleProfessionals[0].Person.Birthday, persistedMovie.Professionals[0].Person.Birthday);
            Assert.AreEqual(sampleProfessionals[0].Person.Name, persistedMovie.Professionals[0].Person.Name);
            Assert.AreEqual(sampleProfessionals[0].Person.Nationality, persistedMovie.Professionals[0].Person.Nationality);
            Assert.AreEqual(sampleProfessionals[0].Ocupation, persistedMovie.Professionals[0].Ocupation);

            Assert.AreEqual(sampleProfessionals[1].Person.Age, persistedMovie.Professionals[1].Person.Age);
            Assert.AreEqual(sampleProfessionals[1].Person.Birthday, persistedMovie.Professionals[1].Person.Birthday);
            Assert.AreEqual(sampleProfessionals[1].Person.Name, persistedMovie.Professionals[1].Person.Name);
            Assert.AreEqual(sampleProfessionals[1].Person.Nationality, persistedMovie.Professionals[1].Person.Nationality);
            Assert.AreEqual(sampleProfessionals[1].Ocupation, persistedMovie.Professionals[1].Ocupation);
        }

        [Test]
        public void Delete_Movie()
        {
            var sampleMovie = dataHelper.GetSampleMovie();

            var movieId = this.movieRepository.Insert(sampleMovie);

            this.movieRepository.Delete(movieId);

            var deletedMovie = this.movieRepository.Get(movieId);

            Assert.IsNull(deletedMovie);
        }
    }
}