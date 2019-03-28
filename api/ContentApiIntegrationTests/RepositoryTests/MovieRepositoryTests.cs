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

        [SetUp]
        public void Setup()
        {
            var connectionString = Configurations.GetConnectionString();
            this.movieRepository = new MovieRepository(connectionString);
            this.mediaRepository = new MediaRepository(connectionString);
            this.personRepository = new PersonRepository(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        private List<Professional> GetSampleProfessionals()
        {
            var professional1 = new Professional()
            {
                Person = new Person()
                {
                    Name = "Arnold Schwarzenegger",
                    Birthday = new DateTime(1947, 7, 30),
                    Nationality = "Austria"
                },
                Ocupation = Ocupation.Actor
            };

            var professional2 = new Professional()
            {
                Person = new Person()
                {
                    Name = "Linda Hamilton",
                    Birthday = new DateTime(1956, 9, 26),
                    Nationality = "United States of America"
                },
                Ocupation = Ocupation.Actor
            };


            return new List<Professional>()
            {
                professional1,
                professional2
            };
        }

        private Media GetSampleCoverImage()
        {
            return new Media()
            {
                Description = "Cover image do Piratas do Caribe",
                Name = "coverimagepiratas",
                Path = "/imges/coverimagepiratas.png",
                Type = MediaType.Image
            };
        }

        private Media GetSampleVideo()
        {
            return new Media()
            {
                Description = "Video do Piratas do Caribe",
                Name = "videopiratas",
                Path = "/videos/videodopiratas.mpd",
                Type = MediaType.Video
            };
        }

        private Movie GetSampleMovie()
        {
            var coverImageSample = GetSampleCoverImage();
            var coverImageId = this.mediaRepository.Insert(coverImageSample);

            var videoSample = GetSampleVideo();
            var videoId = this.mediaRepository.Insert(videoSample);

            var professionalsSample = GetSampleProfessionals();

            var persons = professionalsSample.Select(pro => pro.Person);

            var personsIds = this.personRepository.InsertMany(persons).ToList();

            professionalsSample[0].Person.Id = personsIds[0];
            professionalsSample[1].Person.Id = personsIds[1];

            return new Movie()
            {
                Professionals = professionalsSample,
                CoverImage = new Media() { Id = coverImageId },
                Video = new Media() { Id = videoId },
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
            Assert.AreEqual(sampleMovie.Duration, persistedMovie.Duration);
            Assert.AreEqual(sampleMovie.Name, persistedMovie.Name);
            Assert.AreEqual(sampleMovie.ReleaseDate, persistedMovie.ReleaseDate);
            Assert.AreEqual(sampleMovie.ShortDescription, persistedMovie.ShortDescription);
            Assert.AreEqual(sampleMovie.Studio, persistedMovie.Studio);
            Assert.AreEqual(sampleMovie.Synopsis, persistedMovie.Synopsis);

            // Assert Video Media
            var sampleVideo = GetSampleVideo();
            Assert.AreEqual(sampleVideo.Description, persistedMovie.Video.Description);
            Assert.AreEqual(sampleVideo.Name, persistedMovie.Video.Name);
            Assert.AreEqual(sampleVideo.Path, persistedMovie.Video.Path);
            Assert.AreEqual(sampleVideo.Type, persistedMovie.Video.Type);

            // Assert Cover Image Media
            var sampleCoverImage = GetSampleCoverImage();
            Assert.AreEqual(sampleCoverImage.Description, persistedMovie.CoverImage.Description);
            Assert.AreEqual(sampleCoverImage.Name, persistedMovie.CoverImage.Name);
            Assert.AreEqual(sampleCoverImage.Path, persistedMovie.CoverImage.Path);
            Assert.AreEqual(sampleCoverImage.Type, persistedMovie.CoverImage.Type);

            // Assert Professionals
            var sampleProfessionals = GetSampleProfessionals();
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
            var sampleMovie = GetSampleMovie();

            var movieId = this.movieRepository.Insert(sampleMovie);

            this.movieRepository.Delete(movieId);

            Assert.Throws<InvalidOperationException>(() => this.movieRepository.Get(movieId), "Sequence contains no elements");
        }
    }
}