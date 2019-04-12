using ContentApi.Configurations;
using System;
using System.Collections.Generic;

namespace ContentApi.MediaFiles
{
    public class ContentServerClient : IContentServerClient
    {
        private SSHClient sshClient;

        public ContentServerClient()
        {
            var config = new Configuration().SSHConfiguration;
            this.sshClient = new SSHClient(
                config.Host,
                config.Username,
                config.Password,
                config.Port);
        }

        public ContentServerClient(SSHClient sshclient)
        {
            this.sshClient = sshclient;
        }
        public IList<string> ListFilePaths(string path)
        {
            return this.sshClient.ListFilePaths(path);
        }

        public IList<string> ListFilePathsByExtension(string path, string extension)
        {
            return this.sshClient.ListFilePathsByExtension(path, extension);
        }
    }
}
