using ContentApi;
using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApiIntegrationTests;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContentApiIntegrationTests.ControllerTests
{
    public class MediaControllerTests
    {
        private HttpClient apiClient;
        private DataHelper data;
        private MediaRepository mediaRepository;
        private MovieRepository movieRepository;

        public MediaControllerTests()
        {
            var connectionString = Configurations.GetConnectionString();
            Environment.SetEnvironmentVariable("CONNECTION_STRING", connectionString);
            var server = new TestServer(Program.CreateWebHostBuilder());
            this.apiClient = server.CreateClient();
            this.data = new DataHelper(connectionString);
            this.mediaRepository = new MediaRepository(connectionString);
            this.movieRepository = new MovieRepository(connectionString);
        }

        [Test]
        public async Task Post_Media()
        {
            // Arrange
            var media = data.GetSampleMedia();
            var json = JsonConvert.SerializeObject(media);
            var content = new StringContent(json);

            // Act
            var httpResponse = await this.apiClient.PostAsync("/media", content);
            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            var resultMedia = JsonConvert.DeserializeObject<Media>(responseJson);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsNotNull(resultMedia.Id);
            Assert.AreEqual(media.Name, resultMedia.Name);
            Assert.AreEqual(media.Description, resultMedia.Description);
            Assert.AreEqual(media.Path, resultMedia.Path);
            Assert.AreEqual(media.Type, resultMedia.Type);
        }

        [Test]
        public async Task Get_Medias()
        {
            // Arrange
            var insertsCount = 5;

            data.DeleteAll<Movie>(this.movieRepository);
            data.DeleteAll<Media>(this.mediaRepository);

            var media = data.GetSampleMedia();
            var json = JsonConvert.SerializeObject(media);
            var content = new StringContent(json);

            // Act
            for (int i = 0; i < insertsCount; i++)
            {
                await this.apiClient.PostAsync("/media", content);
            }

            var httpResponse = await this.apiClient.GetAsync($"/media");
            var resultMedias = await HttpResponseHelper.ReadBody<List<Media>>(httpResponse);

            Assert.AreEqual(insertsCount, resultMedias.Count);
        }

        [Test]
        public async Task Get_Media()
        {
            // Arrange
            var media = data.GetSampleMedia();
            var json = JsonConvert.SerializeObject(media);
            var content = new StringContent(json);

            // Act
            var postResult = await this.apiClient.PostAsync("/media", content);
            var postMedia = await HttpResponseHelper.ReadBody<Media>(postResult);
            var httpResponse = await this.apiClient.GetAsync($"/media/{postMedia.Id}");
            var resultMedia = await HttpResponseHelper.ReadBody<Media>(httpResponse);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.IsNotNull(resultMedia.Id);
            Assert.AreEqual(media.Name, resultMedia.Name);
            Assert.AreEqual(media.Description, resultMedia.Description);
            Assert.AreEqual(media.Path, resultMedia.Path);
            Assert.AreEqual(media.Type, resultMedia.Type);
        }

        [Test]
        public async Task Delete_Media()
        {
            // Arrange
            var media = data.GetSampleMedia();
            var json = JsonConvert.SerializeObject(media);
            var content = new StringContent(json);

            // Act
            var resultPost = await this.apiClient.PostAsync("/media", content);
            var resultMedia = await HttpResponseHelper.ReadBody<Media>(resultPost);
            var httpResponse1 = await this.apiClient.DeleteAsync($"/media/{resultMedia.Id}");
            var httpResponse2 = await this.apiClient.DeleteAsync($"/media/{resultMedia.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, httpResponse1.StatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponse2.StatusCode);
        }
    }
}
