using ContentApi.Database;
using ContentApi.Domain;
using ContentApiTests.Helpers;
using NUnit.Framework;
using System.IO;

namespace ContentApiTests
{
    public class ContentControllerTests
    {
        private MovieService movieService;
        [SetUp]
        public void Setup()
        {
            this.movieService = new MovieService("mongodb://localhost:27017");
        }

        [Test]
        public void Should_Post_Movie()
        {
            var cover = FileHelper.GetInputFile("cover.jpeg");

            var movie = new Movie()
            {
                CoverImage = cover,
                Budget = 123000000,
                Country = "EUA",
                Duration = 123123,
                Name = "Piratas do Caribe",
                ShortDescription = "Uns piratas ai muito loucos",
                Studio = "Disney",
                Synopsis = "Jack Sparrow tava fazendo umas baguncinhas no Caribe quando apareceu uma aventura do barulho"
            };

            this.movieService.Post(movie);

            Assert.Pass();
        }

        [Test]
        public void Should_Get_Movie()
        {
            var movie = this.movieService.GetById("5c899810439ddf358c7a0334");

            Assert.AreEqual("Piratas do Caribe", movie.Name);
        }
    }
}