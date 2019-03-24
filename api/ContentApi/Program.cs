using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ContentApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new Configurations();
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls(config.URL)
                .UseStartup<Startup>();
        }
    }
}
