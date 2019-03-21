using ContentApi.Database;
using ContentApi.Domain;
using ContentApiTests.Helpers;
using NUnit.Framework;
using System.IO;

namespace ContentApiTests
{
    public class ContentControllerTests
    {
        private MovieRepository movieRepository;
        [SetUp]
        public void Setup()
        {
            this.movieRepository = new MovieRepository("Server=localhost;Database=content;Uid=content;Pwd=content1234");
        }

        [Test]
        public void Should_Post_Movie()
        {
            //var cover = FileHelper.GetInputFile("cover.jpeg");

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

            this.movieRepository.Insert(movie);

            Assert.Pass();
        }

        [Test]
        public void Should_Get_Movie()
        {
            var movie = this.movieRepository.Get();

            Assert.Pass();
        }
    }
}