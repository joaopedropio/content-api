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
    public class PersonControllerTests
    {
        private HttpClient apiClient;
        private DataHelper data;
        private MovieRepository movieRepository;
        private PersonRepository personRepository;

        public PersonControllerTests()
        {
            var connectionString = Configurations.GetConnectionString();
            Environment.SetEnvironmentVariable("CONNECTION_STRING", connectionString);
            var server = new TestServer(Program.CreateWebHostBuilder());
            this.apiClient = server.CreateClient();
            this.data = new DataHelper(connectionString);
            this.movieRepository = new MovieRepository(connectionString);
            this.personRepository = new PersonRepository(connectionString);
        }

        [Test]
        public async Task Get_Persons()
        {
            // Arrange
            var insertsCount = 5;

            data.DeleteAll<Movie>(this.movieRepository);
            data.DeleteAll<Person>(this.personRepository);

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
        public async Task Post_Person()
        {
            // Arrange
            var person = data.GetSamplePerson();
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json);

            // Act
            var httpResponse = await this.apiClient.PostAsync("/person", content);
            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            var resultPerson = JsonConvert.DeserializeObject<Person>(responseJson);

            // Assert
            Assert.AreEqual(person.Birthday, resultPerson.Birthday);
            Assert.AreEqual(person.Name, resultPerson.Name);
            Assert.AreEqual(person.Nationality, resultPerson.Nationality);
        }

        [Test]
        public async Task Get_Person()
        {

            // Arrange
            var person = data.GetSamplePerson();
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json);

            // Act
            var resultPost = await this.apiClient.PostAsync("/person", content);
            var resultPerson = await HttpResponseHelper.ReadBody<Person>(resultPost);
            var httpResponse = await this.apiClient.GetAsync($"/person/{resultPerson.Id}");
            var persistedPerson = await HttpResponseHelper.ReadBody<Person>(httpResponse);

            // Assert
            Assert.AreEqual(person.Birthday, persistedPerson.Birthday);
            Assert.AreEqual(person.Name, persistedPerson.Name);
            Assert.AreEqual(person.Nationality, persistedPerson.Nationality);
        }

        [Test]
        public async Task Delete_Person()
        {
            // Arrange
            var person = data.GetSamplePerson();
            var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(json);

            // Act
            var resultPost = await this.apiClient.PostAsync("/person", content);
            var resultPerson = await HttpResponseHelper.ReadBody<Person>(resultPost);
            var httpResponse1 = await this.apiClient.DeleteAsync($"/person/{resultPerson.Id}");
            var httpResponse2 = await this.apiClient.DeleteAsync($"/person/{resultPerson.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, httpResponse1.StatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponse2.StatusCode);
        }
    }
}
