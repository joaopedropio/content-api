using ContentApi.Configurations;
using ContentApi.JSON;
using ContentApi.MediaFiles;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace ContentApi.Controllers
{
    public class MediaFilesController : Controller
    {
        private Configuration config;
        private IContentServerClient client;

        public MediaFilesController()
        {
            this.config = new Configuration();
            this.client = new ContentServerClient();
        }

        [Route("/mediafiles")]
        [HttpGet]
        public IActionResult Get()
        {
            var basePath = config.MediaFilesBasePath;
            var mediasPaths = this.client.ListFilePathsByExtension(this.config.MediaFilesBasePath, "mpd");
            var relativePaths = mediasPaths.Select(mp => MediaFileHelper.RemoveBasePath(mp, basePath))
                                           .Where(rl => !string.IsNullOrEmpty(rl));
            return JsonResultHelper.Parse(relativePaths, HttpStatusCode.OK);
        }
    }
}
