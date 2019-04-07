using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApi.JSON;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ContentApi.Controllers
{
    [Produces("application/json")]
    public class PersonController : Controller
    {
        private PersonRepository personRepository;

        public PersonController()
        {
            var config = new Configuration();
            this.personRepository = new PersonRepository(config.ConnectionString);
        }

        [Route("/person")]
        [HttpGet]
        public IActionResult Get([FromQuery] string name)
        {
            IList<Person> persons;
            if (string.IsNullOrEmpty(name))
                persons = this.personRepository.Get();
            else
                persons = this.personRepository.GetByName(name);

            return JsonResultHelper.Parse(persons, HttpStatusCode.OK);
        }

        [Route("/person/{*personId}")]
        [HttpGet]
        public IActionResult Get(uint personId)
        {
            var person = this.personRepository.Get(personId);
            return JsonResultHelper.Parse(person, HttpStatusCode.OK);
        }

        [Route("/person")]
        [HttpPost]
        public IActionResult Post()
        {
            var content = new StreamReader(Request.Body).ReadToEnd();
            var person = JsonConvert.DeserializeObject<Person>(content);
            var personId = this.personRepository.Insert(person);
            person.Id = personId;
            return JsonResultHelper.Parse(person, HttpStatusCode.Created);
        }

        [Route("/person/{*personId}")]
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
