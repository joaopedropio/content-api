using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApiIntegrationTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentApiTests
{
    public class MovieRepositoryTests
    {
        private MovieRepository movieRepository;
        private MediaRepository mediaRepository;
        private PersonRepository personRepository;
        private SampleDataHelper data;

        [SetUp]
        public void Setup()
        {
            var connectionString = Configurations.GetConnectionString();
            this.movieRepository = new MovieRepository(connectionString);
            this.mediaRepository = new MediaRepository(connectionString);
            this.personRepository = new PersonRepository(connectionString);
            this.data = new SampleDataHelper(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        [Test]
        public void Should_Post_Movie()
        {
            var movie = data.GetSampleMovie();

            var movieId = this.movieRepository.Insert(movie);

            Assert.Pass();
        }

        [Test]
        public void Should_Get_Movie()
        {
            var sampleMovie = data.GetSampleMovie();

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
            var sampleVideo = data.GetSampleVideo();
            Assert.AreEqual(sampleVideo.Description, persistedMovie.Video.Description);
            Assert.AreEqual(sampleVideo.Name, persistedMovie.Video.Name);
            Assert.AreEqual(sampleVideo.Path, persistedMovie.Video.Path);
            Assert.AreEqual(sampleVideo.Type, persistedMovie.Video.Type);

            // Assert Cover Image Media
            var sampleCoverImage = data.GetSampleCoverImage();
            Assert.AreEqual(sampleCoverImage.Description, persistedMovie.CoverImage.Description);
            Assert.AreEqual(sampleCoverImage.Name, persistedMovie.CoverImage.Name);
            Assert.AreEqual(sampleCoverImage.Path, persistedMovie.CoverImage.Path);
            Assert.AreEqual(sampleCoverImage.Type, persistedMovie.CoverImage.Type);

            // Assert Professionals
            var sampleProfessionals = data.GetSampleProfessionals();
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
        public void Should_Delete_Movie()
        {
            var sampleMovie = data.GetSampleMovie();

            var movieId = this.movieRepository.Insert(sampleMovie);

            this.movieRepository.Delete(movieId);

            Assert.Throws<InvalidOperationException>(() => this.movieRepository.Get(movieId), "Sequence contains no elements");
        }
    }
}