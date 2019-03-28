using Microsoft.Extensions.Configuration;

namespace ContentApiIntegrationTests
{
    public static class Configurations
    {
        private static IConfiguration Configuration => new ConfigurationBuilder().AddEnvironmentVariables().Build();
        public static string GetConnectionString()
        {
            var connectionString = Configuration.GetValue<string>("CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
                return "Server=localhost;Database=content;Uid=content;Pwd=content1234";

            return connectionString;
        }
    }
}
