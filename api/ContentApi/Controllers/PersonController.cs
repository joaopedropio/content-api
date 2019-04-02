﻿using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApi.JSON;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace ContentApi.Controllers
{
    [Route("/person/{*personId}")]
    [Produces("application/json")]
    public class PersonController : Controller
    {
        private PersonRepository personRepository;

        public PersonController()
        {
            var config = new Configuration();
            this.personRepository = new PersonRepository(config.ConnectionString);
        }

        [HttpGet]
        public IActionResult Get(uint personId)
        {
            var person = this.personRepository.Get(personId);
            return JsonResultHelper.Parse(person, HttpStatusCode.OK);
        }

        [HttpPost]
        public IActionResult Post()
        {
            var content = new StreamReader(Request.Body).ReadToEnd();
            var person = JsonConvert.DeserializeObject<Person>(content);
            var personId = this.personRepository.Insert(person);
            person.Id = personId;
            return JsonResultHelper.Parse(person, HttpStatusCode.Created);
        }

        [HttpDelete]
        public IActionResult Delete(uint personId)
        {
            try
            {
                this.personRepository.Delete(personId);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.NotFound.GetHashCode());
            }
            return StatusCode(HttpStatusCode.NoContent.GetHashCode());
        }
    }
}