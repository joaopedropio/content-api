﻿using ContentClient;
using ContentClient.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ContentClientIntegrationTests
{
    public class ContentClientTests
    {
        private ContentClient.ContentClient client;

        public ContentClientTests()
        {
            var contentApiBaseAddress = Configurations.GetBaseAddress();
            this.client = new ContentClient.ContentClient(contentApiBaseAddress);
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
        public async Task Post()
        {
            var person = GetSamplePerson();
            var persistedPerson = await this.client.Insert<Person>(person);

            Assert.IsNotNull(persistedPerson);
        }

        [Test]
        public async Task Get()
        {
            var person = GetSamplePerson();
            var personId = await this.client.Insert<Person>(person);
            var resultPerson = await this.client.Get<Person>(personId);

            Assert.IsNotNull(resultPerson);
            Assert.AreEqual(person.Name, resultPerson.Name);
            Assert.AreEqual(person.Birthday, resultPerson.Birthday);
            Assert.AreEqual(person.Nationality, resultPerson.Nationality);
        }

        [Test]
        public async Task Delete()
        {
            var person = GetSamplePerson();
            var personId = await this.client.Insert<Person>(person);
            await this.client.Delete<Person>(personId);
            Assert.ThrowsAsync<ArgumentException>(() => this.client.Delete<Person>(personId));
        }
    }
}
