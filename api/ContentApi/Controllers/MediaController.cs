using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApi.JSON;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace ContentApi.Controllers
{
    [Route("/media/{*mediaId}")]
    [Produces("application/json")]
    public class MediaController : Controller
    {
        private MediaRepository mediaRepository;

        public MediaController()
        {
            var config = new Configuration();
            this.mediaRepository = new MediaRepository(config.ConnectionString);
        }

        [HttpGet]
        public IActionResult Get(uint mediaId)
        {
            var media = this.mediaRepository.Get(mediaId);
            return JsonResultHelper.Parse(media, HttpStatusCode.OK);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var content = new StreamReader(Request.Body).ReadToEnd();
            var media = JsonConvert.DeserializeObject<Media>(content);
            var mediaId = this.mediaRepository.Insert(media);
            media.Id = mediaId;
            return JsonResultHelper.Parse(media, HttpStatusCode.Created);
        }

        [HttpDelete]
        public IActionResult Delete(uint mediaId)
        {
            try
            {
                this.mediaRepository.Delete(mediaId);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.NotFound.GetHashCode());
            }
            return StatusCode(HttpStatusCode.NoContent.GetHashCode());
        }
    }
}
