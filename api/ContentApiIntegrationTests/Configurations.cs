using ContentApi.Configurations;
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

        public static SSHConfiguration GetSSHConfigurations()
        {
            return new SSHConfiguration()
            {
                Host = Configuration.GetValue<string>("SSH_HOST") ?? "localhost",
                Port = int.Parse(Configuration.GetValue<string>("SSH_PORT") ?? "2222"),
                Username = Configuration.GetValue<string>("SSH_USERNAME") ?? "content",
                Password = Configuration.GetValue<string>("SSH_PASSWORD") ?? "password",
            };
        }

        public static string GetMediaFilesBasePath()
        {
            return Configuration.GetValue<string>("MEDIAFILES_BASE_PATH") ?? "/content";
        }

    }
}
