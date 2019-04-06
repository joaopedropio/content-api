using Microsoft.Extensions.Configuration;
using System;

namespace ContentClientIntegrationTests
{
    public static class Configurations
    {
        private static IConfiguration Configuration => new ConfigurationBuilder().AddEnvironmentVariables().Build();
        public static string GetBaseAddress()
        {
            var baseAdrress = Configuration.GetValue<string>("CONTENT_API_BASE_ADDRESS");
            if (string.IsNullOrEmpty(baseAdrress))
                return "http://localhost";

            return baseAdrress;
        }
    }
}