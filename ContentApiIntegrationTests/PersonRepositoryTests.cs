using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using NUnit.Framework;
using System;

namespace ContentApiTests
{
    public class PersonRepositoryTests
    {
        private PersonRepository personRepository;
        [SetUp]
        public void Setup()
        {
            var connectionString = "Server=localhost;Database=content;Uid=content;Pwd=content1234";
            this.personRepository = new PersonRepository(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        private Person GetSamplePerson()
        {
            return new Person()
            {
                Name = "Arnold Schwarzenegger",
                Birthday = new DateTime(1947, 7, 30),
                Nationality = "Austria"
            };
        }

        [Test]
        public void Insert_Person()
        {
            var person = GetSamplePerson();

            var personId = this.personRepository.Insert(person);

            Assert.Pass();
        }

        [Test]
        public void Get_Person()
        {
            var person = GetSamplePerson();

            var personId = this.personRepository.Insert(person);

            var personPersisted = this.personRepository.Get(personId);

            Assert.AreEqual(person.Birthday, personPersisted.Birthday);
            Assert.AreEqual(person.Name, personPersisted.Name);
            Assert.AreEqual(person.Nationality, personPersisted.Nationality);
        }
    }
}
