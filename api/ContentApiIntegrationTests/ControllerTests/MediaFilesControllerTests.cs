using ContentApi;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContentApiIntegrationTests.ControllerTests
{
    public class MediaFilesControllerTests
    {
        private HttpClient apiClient;

        public MediaFilesControllerTests()
        {
            var connectionString = Configurations.GetConnectionString();
            Environment.SetEnvironmentVariable("CONNECTION_STRING", connectionString);
            var server = new TestServer(Program.CreateWebHostBuilder());
            this.apiClient = server.CreateClient();
        }

        [Test]
        public async Task Should_GetMediaFilesPaths()
        {
            var httpResponse = await this.apiClient.GetAsync("/mediafiles");
            var mediaFilesPaths = await HttpResponseHelper.ReadBody<List<string>>(httpResponse);

            Assert.IsNotNull(mediaFilesPaths);
            Assert.Pass();
        }
    }
}
