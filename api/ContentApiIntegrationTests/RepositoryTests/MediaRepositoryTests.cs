using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApiIntegrationTests;
using NUnit.Framework;
using System;

namespace ContentApiTests
{
    public class MediaRepositoryTests
    {
        private MediaRepository mediaRepository;
        private SampleDataHelper data;

        public MediaRepositoryTests()
        {
            var connectionString = Configurations.GetConnectionString();
            this.mediaRepository = new MediaRepository(connectionString);
            this.data = new SampleDataHelper(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        [Test]
        public void Insert_Media()
        {
            var media = data.GetSampleMedia();

            this.mediaRepository.Insert(media);

            Assert.Pass();
        }

        [Test]
        public void Get_Media()
        {
            var media = data.GetSampleMedia();

            var mediaId = this.mediaRepository.Insert(media);

            var mediaPersisted = this.mediaRepository.Get(mediaId);

            Assert.AreEqual(media.Description, mediaPersisted.Description);
            Assert.AreEqual(media.Name, mediaPersisted.Name);
            Assert.AreEqual(media.Path, mediaPersisted.Path);
            Assert.AreEqual(media.Type, mediaPersisted.Type);
        }

        [Test]
        public void Delete_Media()
        {
            var media = data.GetSampleMedia();

            var mediaId = this.mediaRepository.Insert(media);

            this.mediaRepository.Delete(mediaId);

            Assert.Throws<ArgumentException>(() => this.mediaRepository.Get(mediaId));
        }
    }
}
