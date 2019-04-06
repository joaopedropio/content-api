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
    [Produces("application/json")]
    public class MediaController : Controller
    {
        private MediaRepository mediaRepository;

        public MediaController()
        {
            var config = new Configuration();
            this.mediaRepository = new MediaRepository(config.ConnectionString);
        }

        [Route("/media")]
        [HttpGet]
        public IActionResult Get()
        {
            var medias = this.mediaRepository.Get();
            return JsonResultHelper.Parse(medias, HttpStatusCode.OK);
        }

        [Route("/media/{*mediaId}")]
        [HttpGet]
        public IActionResult Get(uint mediaId)
        {
            var media = this.mediaRepository.Get(mediaId);
            return JsonResultHelper.Parse(media, HttpStatusCode.OK);
        }


        [Route("/media")]
        [HttpPost]
        public IActionResult Post()
        {
            var content = new StreamReader(Request.Body).ReadToEnd();
            var media = JsonConvert.DeserializeObject<Media>(content);
            var mediaId = this.mediaRepository.Insert(media);
            media.Id = mediaId;
            return JsonResultHelper.Parse(media, HttpStatusCode.Created);
        }


        [Route("/media/{*mediaId}")]
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
