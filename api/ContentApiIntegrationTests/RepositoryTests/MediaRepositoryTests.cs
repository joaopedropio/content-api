using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApiIntegrationTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentApiTests.RepositoryTests
{
    public class MediaRepositoryTests
    {
        private MediaRepository mediaRepository;
        private MovieRepository movieRepository;
        private DataHelper dataHelper;

        public MediaRepositoryTests()
        {
            var connectionString = Configurations.GetConnectionString();
            this.mediaRepository = new MediaRepository(connectionString);
            this.movieRepository = new MovieRepository(connectionString);
            this.dataHelper = new DataHelper(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        [Test]
        public void Insert_Media()
        {
            var media = dataHelper.GetSampleMedia();

            this.mediaRepository.Insert(media);

            Assert.Pass();
        }

        [Test]
        public void Get_Medias()
        {
            // Has to delete parent too
            dataHelper.DeleteAll<Movie>(this.movieRepository);
            dataHelper.DeleteAll<Media>(this.mediaRepository);

            var media = dataHelper.GetSampleMedia();

            var insertsCount = 5;

            for (int i = 0; i < insertsCount; i++)
            {
                this.mediaRepository.Insert(media);
            }

            var medias = this.mediaRepository.Get();

            Assert.AreEqual(medias.Count, insertsCount);
        }

        [Test]
        public void Get_Media()
        {
            var media = dataHelper.GetSampleMedia();

            var mediaId = this.mediaRepository.Insert(media);

            var mediaPersisted = this.mediaRepository.Get(mediaId);

            Assert.AreEqual(media.Description, mediaPersisted.Description);
            Assert.AreEqual(media.Name, mediaPersisted.Name);
            Assert.AreEqual(media.Path, mediaPersisted.Path);
            Assert.AreEqual(media.Type, mediaPersisted.Type);
        }

        [Test]
        public void Get_MediaByName()
        {
            var media = dataHelper.GetSampleMedia();
            media.Name = "um nome de media qualquer";

            this.mediaRepository.Insert(media);

            var medias = this.mediaRepository.GetByName(media.Name);

            var mediaPersisted = medias.FirstOrDefault();

            Assert.GreaterOrEqual(medias.Count, 1);
            Assert.AreEqual(media.Description, mediaPersisted.Description);
            Assert.AreEqual(media.Name, mediaPersisted.Name);
            Assert.AreEqual(media.Path, mediaPersisted.Path);
            Assert.AreEqual(media.Type, mediaPersisted.Type);
        }

        [Test]
        public void Delete_Media()
        {
            var media = dataHelper.GetSampleMedia();

            var mediaId = this.mediaRepository.Insert(media);

            this.mediaRepository.Delete(mediaId);

            var deletedMedia = this.mediaRepository.Get(mediaId);

            Assert.IsNull(deletedMedia);
        }
    }
}
