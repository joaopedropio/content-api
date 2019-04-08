using Microsoft.Extensions.Configuration;
using System;

namespace ContentApi.Configurations
{
    public class Configuration
    {
        public string Port { get; private set; }
        public string Domain { get; private set; }
        public string URL { get; private set; }
        public string ConnectionString { get; private set; }
        public string MediaFilesBasePath { get; private set; }
        public SSHConfiguration SSHConfiguration { get; private set; }

        public Configuration() : this(new ConfigurationBuilder().AddEnvironmentVariables().Build()) { }

        public Configuration(IConfigurationRoot configuration)
        {
            Domain = configuration.GetValue<string>("API_DOMAIN") ?? "*";
            Port = configuration.GetValue<string>("API_PORT") ?? "80";
            URL = string.Format($"http://{this.Domain}:{this.Port}");
            ConnectionString = configuration.GetValue<string>("CONNECTION_STRING");
            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("No connection string provided.");

            MediaFilesBasePath = configuration.GetValue<string>("MEDIAFILES_BASE_PATH") ?? "/content";

            SSHConfiguration = new SSHConfiguration()
            {
                Host = configuration.GetValue<string>("SSH_HOST") ?? "localhost",
                Port = int.Parse(configuration.GetValue<string>("SSH_PORT") ?? "2222"),
                Username = configuration.GetValue<string>("SSH_USERNAME") ?? "content",
                Password = configuration.GetValue<string>("SSH_PASSWORD") ?? "password",
            };
        }
    }
}