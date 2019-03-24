using Microsoft.Extensions.Configuration;
using System;

namespace ContentApi
{
    public class Configurations
    {
        public string Port { get; private set; }
        public string Domain { get; private set; }
        public string URL { get; private set; }
        public string ConnectionString { get; set; }

        public Configurations() : this(new ConfigurationBuilder().AddEnvironmentVariables().Build()) { }

        public Configurations(IConfigurationRoot configuration)
        {
            Domain = configuration.GetValue<string>("API_DOMAIN") ?? "*";
            Port = configuration.GetValue<string>("API_PORT") ?? "80";
            URL = string.Format($"http://{this.Domain}:{this.Port}");
            ConnectionString = configuration.GetValue<string>("CONNECTION_STRING");
            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("No connection string provided.");
        }
    }
}