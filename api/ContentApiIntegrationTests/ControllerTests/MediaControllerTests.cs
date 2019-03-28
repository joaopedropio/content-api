using ContentApi;
using ContentApi.Domain.Entities;
using ContentApiIntegrationTests;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContentApiIntegrationTest
{
    public class MediaControllerTests
    {
        private HttpClient apiClient;
        public MediaControllerTests()
        {
            Environment.SetEnvironmentVariable("CONNECTION_STRING", Configurations.ConnectionString);
            var server = new TestServer(Program.CreateWebHostBuilder());
            this.apiClient = server.CreateClient();
        }

        public Media GetSampleMedia()
        {
            return new Media()
            {
                Description = "Isso aqui é uma imagem muito legal",
                Name = "rosa",
                Path = "/images/rosa.png",
                Type = MediaType.Image
            };
        }

        [Test]
        public async Task PostMedia()
        {
            // Arrange
            var media = GetSampleMedia();
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
    }
}
