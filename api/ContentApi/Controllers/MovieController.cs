using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApi.JSON;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ContentApi.Controllers
{
    [Produces("application/json")]
    public class MovieController : Controller
    {
        private MovieRepository movieRepository;

        public MovieController()
        {
            var config = new Configuration();
            this.movieRepository = new MovieRepository(config.ConnectionString);
        }

        [Route("/movie")]
        [HttpGet]
        public IActionResult Get([FromQuery] string name)
        {
            IList<Movie> movies;
            if (string.IsNullOrEmpty(name))
                movies = this.movieRepository.Get();
            else
                movies = this.movieRepository.GetByName(name);

            return JsonResultHelper.Parse(movies, HttpStatusCode.OK);
        }

        [Route("/movie/{*movieId}")]
        [HttpGet]
        public IActionResult Get(uint movieId)
        {
            var movie = this.movieRepository.Get(movieId);
            return JsonResultHelper.Parse(movie, HttpStatusCode.OK);
        }

        [Route("/movie")]
        [HttpPost]
        public IActionResult Post()
        {
            var content = new StreamReader(Request.Body).ReadToEnd();
            var movie = JsonConvert.DeserializeObject<Movie>(content);
            var movieId = this.movieRepository.Insert(movie);
            movie.Id = movieId;
            return JsonResultHelper.Parse(movie, HttpStatusCode.Created);
        }

        [Route("/movie/{*movieId}")]
        [HttpDelete]
        public IActionResult Delete(uint movieId)
        {
            try
            {
                this.movieRepository.Delete(movieId);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.NotFound.GetHashCode());
            }
            return StatusCode(HttpStatusCode.NoContent.GetHashCode());
        }
    }
}
