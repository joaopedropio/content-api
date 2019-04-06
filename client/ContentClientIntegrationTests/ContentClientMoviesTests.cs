using ContentClient;
using ContentClient.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentClientIntegrationTests
{
    public class ContentClientMoviesTests
    {
        private ContentClient.ContentClient client;

        public ContentClientMoviesTests()
        {
            var contentApiBaseAddress = Configurations.GetBaseAddress();
            this.client = new ContentClient.ContentClient(contentApiBaseAddress);
        }

        public List<Professional> GetSampleProfessionals()
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

        public Media GetSampleCoverImage()
        {
            return new Media()
            {
                Description = "Cover image do Piratas do Caribe",
                Name = "coverimagepiratas",
                Path = "/imges/coverimagepiratas.png",
                Type = MediaType.Image
            };
        }

        public Media GetSampleVideo()
        {
            return new Media()
            {
                Description = "Video do Piratas do Caribe",
                Name = "videopiratas",
                Path = "/videos/videodopiratas.mpd",
                Type = MediaType.Video
            };
        }

        public async Task<Movie> GetSampleMovie()
        {
            var coverImageSample = GetSampleCoverImage();
            var coverImageId = await this.client.Insert(coverImageSample);
            coverImageSample.Id = coverImageId;

            var videoSample = GetSampleVideo();
            var videoId = await this.client.Insert(videoSample);
            videoSample.Id = videoId;

            var professionalsSample = GetSampleProfessionals();

            for (int i = 0; i < professionalsSample.Count; i++)
            {
                var person = professionalsSample[i].Person;
                var personId = await this.client.Insert(person);
                professionalsSample[i].Person.Id = personId;
            }

            return new Movie()
            {
                Professionals = professionalsSample,
                CoverImage = coverImageSample,
                Video = videoSample,
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
        public async Task Post()
        {
            var movie = await GetSampleMovie();
            var persistedMovie = await this.client.Insert<Movie>(movie);

            Assert.IsNotNull(persistedMovie);
        }

        [Test]
        public async Task Get()
        {
            var movie = await GetSampleMovie();
            var movieId = await this.client.Insert<Movie>(movie);
            var resultMovie = await this.client.Get<Movie>(movieId);

            Assert.IsNotNull(resultMovie.Id);
            Assert.AreEqual(movie.Name, resultMovie.Name);
            Assert.AreEqual(movie.ReleaseDate, resultMovie.ReleaseDate);
            Assert.AreEqual(movie.ShortDescription, resultMovie.ShortDescription);
            Assert.AreEqual(movie.Studio, resultMovie.Studio);
            Assert.AreEqual(movie.Synopsis, resultMovie.Synopsis);

            // Assert Professionals
            Assert.AreEqual(movie.Professionals[0].Ocupation, resultMovie.Professionals[0].Ocupation);
            Assert.AreEqual(movie.Professionals[0].Person.Name, resultMovie.Professionals[0].Person.Name);
            Assert.AreEqual(movie.Professionals[0].Person.Birthday, resultMovie.Professionals[0].Person.Birthday);
            Assert.AreEqual(movie.Professionals[0].Person.Name, resultMovie.Professionals[0].Person.Name);
            Assert.AreEqual(movie.Professionals[0].Person.Nationality, resultMovie.Professionals[0].Person.Nationality);

            Assert.AreEqual(movie.Professionals[1].Ocupation, resultMovie.Professionals[1].Ocupation);
            Assert.AreEqual(movie.Professionals[1].Person.Name, resultMovie.Professionals[1].Person.Name);
            Assert.AreEqual(movie.Professionals[1].Person.Birthday, resultMovie.Professionals[1].Person.Birthday);
            Assert.AreEqual(movie.Professionals[1].Person.Name, resultMovie.Professionals[1].Person.Name);
            Assert.AreEqual(movie.Professionals[1].Person.Nationality, resultMovie.Professionals[1].Person.Nationality);
            // Assert Video
            Assert.AreEqual(movie.Video.Description, resultMovie.Video.Description);
            Assert.AreEqual(movie.Video.Name, resultMovie.Video.Name);
            Assert.AreEqual(movie.Video.Path, resultMovie.Video.Path);
            Assert.AreEqual(movie.Video.Type, resultMovie.Video.Type);

            // Assert CoverImage
            Assert.AreEqual(movie.CoverImage.Description, movie.CoverImage.Description);
            Assert.AreEqual(movie.CoverImage.Name, movie.CoverImage.Name);
            Assert.AreEqual(movie.CoverImage.Path, movie.CoverImage.Path);
            Assert.AreEqual(movie.CoverImage.Type, movie.CoverImage.Type);
        }

        [Test]
        public async Task Delete()
        {
            var movie = await GetSampleMovie();
            var movieId = await this.client.Insert<Movie>(movie);
            await this.client.Delete<Movie>(movieId);
            Assert.ThrowsAsync<ArgumentException>(() => this.client.Delete<Movie>(movieId));
        }
    }
}
