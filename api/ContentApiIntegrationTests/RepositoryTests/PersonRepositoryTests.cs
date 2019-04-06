using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApiIntegrationTests;
using NUnit.Framework;
using System;

namespace ContentApiTests.RepositoryTests
{
    public class PersonRepositoryTests
    {
        private PersonRepository personRepository;
        private MovieRepository movieRepository;
        private DataHelper dataHelper;

        [SetUp]
        public void Setup()
        {
            var connectionString = Configurations.GetConnectionString();
            this.personRepository = new PersonRepository(connectionString);
            this.movieRepository = new MovieRepository(connectionString);
            this.dataHelper = new DataHelper(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        [Test]
        public void Get_Persons()
        {
            // Has to delete parent too
            dataHelper.DeleteAll<Movie>(this.movieRepository);
            dataHelper.DeleteAll<Person>(this.personRepository);

            var person = dataHelper.GetSamplePerson();

            var insertsCount = 5;

            for (int i = 0; i < insertsCount; i++)
            {
                this.personRepository.Insert(person);
            }

            var persons = this.personRepository.Get();

            Assert.AreEqual(persons.Count, insertsCount);
        }

        [Test]
        public void Insert_Person()
        {
            var person = dataHelper.GetSamplePerson();

            var personId = this.personRepository.Insert(person);

            Assert.Pass();
        }

        [Test]
        public void Get_Person()
        {
            var person = dataHelper.GetSamplePerson();

            var personId = this.personRepository.Insert(person);

            var personPersisted = this.personRepository.Get(personId);

            Assert.AreEqual(person.Birthday, personPersisted.Birthday);
            Assert.AreEqual(person.Name, personPersisted.Name);
            Assert.AreEqual(person.Nationality, personPersisted.Nationality);
        }

        [Test]
        public void Delete_Person()
        {
            var samplePerson = dataHelper.GetSamplePerson();

            var personId = this.personRepository.Insert(samplePerson);

            this.personRepository.Delete(personId);

            Assert.Throws<InvalidOperationException>(() => this.personRepository.Get(personId), "Sequence contains no elements");
        }
    }
}
