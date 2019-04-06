using ContentApi;
using ContentApi.Domain.Entities;
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

        public PersonControllerTests()
        {
            var connectionString = Configurations.GetConnectionString();
            Environment.SetEnvironmentVariable("CONNECTION_STRING", connectionString);
            var server = new TestServer(Program.CreateWebHostBuilder());
            this.apiClient = server.CreateClient();
            this.data = new DataHelper(connectionString);
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
