using ContentApi.Domain.Repositories;
using ContentApi.JSON;
using Microsoft.AspNetCore.Mvc;
using static System.Net.HttpStatusCode;

namespace ContentApi.Controllers
{
    [Route("/media/{*mediaId}")]
    [Produces("application/json")]
    public class MediaController : Controller
    {
        private MediaRepository mediaRepository;

        public MediaController()
        {
            var config = new Configurations();
            this.mediaRepository = new MediaRepository(config.ConnectionString);
        }

        [HttpGet]
        public IActionResult Get(uint mediaId)
        {
            var media = this.mediaRepository.Get(mediaId);
            return JsonResultHelper.Parse(media, OK);
        }
    }
}
