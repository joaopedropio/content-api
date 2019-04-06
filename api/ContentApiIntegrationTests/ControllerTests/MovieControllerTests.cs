using ContentApi;
using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContentApiIntegrationTests.ControllerTests
{
    public class MovieControllerTests
    {
        private HttpClient apiClient;
        private DataHelper data;
        private MovieRepository movieRepository;

        public MovieControllerTests()
        {
            var connectionString = Configurations.GetConnectionString();
            Environment.SetEnvironmentVariable("CONNECTION_STRING", connectionString);
            var server = new TestServer(Program.CreateWebHostBuilder());
            this.apiClient = server.CreateClient();
            this.data = new DataHelper(connectionString);
            this.movieRepository = new MovieRepository(connectionString);
        }

        [Test]
        public async Task Get_Movies()
        {
            // Arrange
            var insertsCount = 5;

            data.DeleteAll<Movie>(this.movieRepository);

            var movie = data.GetSampleMovie();
            var json = JsonConvert.SerializeObject(movie);
            var content = new StringContent(json);

            // Act
            for (int i = 0; i < insertsCount; i++)
            {
                await this.apiClient.PostAsync("/movie", content);
            }

            var httpResponse = await this.apiClient.GetAsync($"/movie");
            var resultMovies = await HttpResponseHelper.ReadBody<List<Movie>>(httpResponse);

            Assert.AreEqual(insertsCount, resultMovies.Count);
        }

        [Test]
        public async Task PostMovie()
        {
            // Arrange
            var movie = data.GetSampleMovie();
            var json = JsonConvert.SerializeObject(movie);
            var content = new StringContent(json);

            // Act
            var httpResponse = await this.apiClient.PostAsync("/movie", content);
            var resultMovie = await HttpResponseHelper.ReadBody<Movie>(httpResponse);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsNotNull(resultMovie.Id);
            Assert.AreEqual(movie.Name, resultMovie.Name);
            Assert.AreEqual(movie.ReleaseDate, resultMovie.ReleaseDate);
            Assert.AreEqual(movie.ShortDescription, resultMovie.ShortDescription);
            Assert.AreEqual(movie.Studio, resultMovie.Studio);
            Assert.AreEqual(movie.Synopsis, resultMovie.Synopsis);

            // Assert Professionals
            Assert.AreEqual(movie.Professionals[0].Ocupation, resultMovie.Professionals[0].Ocupation);
            Assert.AreEqual(movie.Professionals[0].Person.Name, resultMovie.Professionals[0].Person.Name);
            Assert.AreEqual(movie.Professionals[0].Person.Age, resultMovie.Professionals[0].Person.Age);
            Assert.AreEqual(movie.Professionals[0].Person.Birthday, resultMovie.Professionals[0].Person.Birthday);
            Assert.AreEqual(movie.Professionals[0].Person.Name, resultMovie.Professionals[0].Person.Name);
            Assert.AreEqual(movie.Professionals[0].Person.Nationality, resultMovie.Professionals[0].Person.Nationality);

            Assert.AreEqual(movie.Professionals[1].Ocupation, resultMovie.Professionals[1].Ocupation);
            Assert.AreEqual(movie.Professionals[1].Person.Name, resultMovie.Professionals[1].Person.Name);
            Assert.AreEqual(movie.Professionals[1].Person.Age, resultMovie.Professionals[1].Person.Age);
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
        public async Task Get_Movie()
        {
            // Arrange
            var movie = data.GetSampleMovie();
            var json = JsonConvert.SerializeObject(movie);
            var content = new StringContent(json);

            // Act
            var postResult = await this.apiClient.PostAsync("/movie", content);
            var postMovie = await HttpResponseHelper.ReadBody<Movie>(postResult);
            var httpResponse = await this.apiClient.GetAsync($"/movie/{postMovie.Id}");
            var resultMovie = await HttpResponseHelper.ReadBody<Movie>(httpResponse);


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.IsNotNull(resultMovie.Id);
            Assert.AreEqual(movie.Name, resultMovie.Name);
            Assert.AreEqual(movie.ReleaseDate, resultMovie.ReleaseDate);
            Assert.AreEqual(movie.ShortDescription, resultMovie.ShortDescription);
            Assert.AreEqual(movie.Studio, resultMovie.Studio);
            Assert.AreEqual(movie.Synopsis, resultMovie.Synopsis);

            // Assert Professionals
            Assert.AreEqual(movie.Professionals[0].Ocupation, resultMovie.Professionals[0].Ocupation);
            Assert.AreEqual(movie.Professionals[0].Person.Name, resultMovie.Professionals[0].Person.Name);
            Assert.AreEqual(movie.Professionals[0].Person.Age, resultMovie.Professionals[0].Person.Age);
            Assert.AreEqual(movie.Professionals[0].Person.Birthday, resultMovie.Professionals[0].Person.Birthday);
            Assert.AreEqual(movie.Professionals[0].Person.Name, resultMovie.Professionals[0].Person.Name);
            Assert.AreEqual(movie.Professionals[0].Person.Nationality, resultMovie.Professionals[0].Person.Nationality);

            Assert.AreEqual(movie.Professionals[1].Ocupation, resultMovie.Professionals[1].Ocupation);
            Assert.AreEqual(movie.Professionals[1].Person.Name, resultMovie.Professionals[1].Person.Name);
            Assert.AreEqual(movie.Professionals[1].Person.Age, resultMovie.Professionals[1].Person.Age);
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
        public async Task Delete_Movie()
        {
            // Arrange
            var movie = data.GetSampleMovie();
            var json = JsonConvert.SerializeObject(movie);
            var content = new StringContent(json);

            // Act
            var resultPost = await this.apiClient.PostAsync("/movie", content);
            var resultMovie = await HttpResponseHelper.ReadBody<Movie>(resultPost);
            var httpResponse1 = await this.apiClient.DeleteAsync($"/movie/{resultMovie.Id}");
            var httpResponse2 = await this.apiClient.DeleteAsync($"/movie/{resultMovie.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, httpResponse1.StatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponse2.StatusCode);
        }
    }
}
