using ContentApi.Configurations;
using ContentApi.JSON;
using ContentApi.MediaFiles;
using Microsoft.AspNetCore.Mvc;
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
            var mediasPaths = this.client.ListFilePaths(this.config.MediaFilesBasePath);
            return JsonResultHelper.Parse(mediasPaths, HttpStatusCode.OK);
        }
    }
}
