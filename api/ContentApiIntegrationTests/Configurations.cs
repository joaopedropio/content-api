using Microsoft.Extensions.Configuration;
using System;

namespace ContentApiIntegrationTests
{
    public class Configurations
    {
        public string ConnectionString { get; set; }

        public Configurations() : this(new ConfigurationBuilder().AddEnvironmentVariables().Build()) { }

        public Configurations(IConfigurationRoot configuration)
        {
            ConnectionString = configuration.GetValue<string>("CONNECTION_STRING");
            if (string.IsNullOrEmpty(ConnectionString))
                throw new Exception("No connection string provided.");
        }
    }
}