using ContentApi.MediaFiles;
using NUnit.Framework;

namespace ContentApiIntegrationTests.SSHClientTests
{
    public class SSHClientTests
    {
        private SSHClient sshClient;

        public SSHClientTests()
        {
            var config = Configurations.GetSSHConfigurations();
            this.sshClient = new SSHClient(
                config.Host,
                config.Username,
                config.Password,
                config.Port);
        }

        [Test]
        public void Should_FilePaths()
        {
            var paths = this.sshClient.ListFilePaths("/content");
            Assert.Pass();
        }
    }
}
