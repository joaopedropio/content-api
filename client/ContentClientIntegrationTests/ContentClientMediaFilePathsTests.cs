using NUnit.Framework;
using System.Threading.Tasks;

namespace ContentClientIntegrationTests
{
    public class ContentClientMediaFilePathsTests
    {
        private ContentClient.ContentClient client;

        public ContentClientMediaFilePathsTests()
        {
            var contentApiBaseAddress = Configurations.GetBaseAddress();
            this.client = new ContentClient.ContentClient(contentApiBaseAddress);
        }
        [Test]
        public async Task GetMediaFilePaths()
        {
            var mediaFilePaths = await this.client.GetMediaFilesPaths();

            Assert.IsNotNull(mediaFilePaths);
        }
    }
}
