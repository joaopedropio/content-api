using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApiIntegrationTests;
using NUnit.Framework;
using System;

namespace ContentApiTests
{
    public class PersonRepositoryTests
    {
        private PersonRepository personRepository;
        private SampleDataHelper data;

        [SetUp]
        public void Setup()
        {
            var connectionString = Configurations.GetConnectionString();
            this.personRepository = new PersonRepository(connectionString);
            this.data = new SampleDataHelper(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        [Test]
        public void Insert_Person()
        {
            var person = data.GetSamplePerson();

            var personId = this.personRepository.Insert(person);

            Assert.Pass();
        }

        [Test]
        public void Get_Person()
        {
            var person = data.GetSamplePerson();

            var personId = this.personRepository.Insert(person);

            var personPersisted = this.personRepository.Get(personId);

            Assert.AreEqual(person.Birthday, personPersisted.Birthday);
            Assert.AreEqual(person.Name, personPersisted.Name);
            Assert.AreEqual(person.Nationality, personPersisted.Nationality);
        }

        [Test]
        public void Delete_Person()
        {
            var samplePerson = data.GetSamplePerson();

            var personId = this.personRepository.Insert(samplePerson);

            this.personRepository.Delete(personId);

            Assert.Throws<InvalidOperationException>(() => this.personRepository.Get(personId), "Sequence contains no elements");
        }
    }
}
