using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using NUnit.Framework;

namespace ContentApiTests
{
    public class MediaRepositoryTests
    {
        private MediaRepository mediaRepository;

        public MediaRepositoryTests()
        {
            var connectionString = "Server=localhost;Database=content;Uid=content;Pwd=content1234";
            this.mediaRepository = new MediaRepository(connectionString);
            ContentApi.Database.DatabaseSetup.Bootstrap(connectionString);
        }

        public Media GetSampleMedia()
        {
            return new Media()
            {
                Description = "Isso aqui é uma imagem muito legal",
                Name = "rosa",
                Path = "/images/rosa.png",
                Type = MediaType.Image
            };
        }

        [Test]
        public void Insert_Media()
        {
            var media = GetSampleMedia();

            this.mediaRepository.Insert(media);

            Assert.Pass();
        }

        [Test]
        public void Get_Media()
        {
            var media = GetSampleMedia();

            var mediaId = this.mediaRepository.Insert(media);

            var mediaPersisted = this.mediaRepository.Get(mediaId);

            Assert.AreEqual(media.Description, mediaPersisted.Description);
            Assert.AreEqual(media.Name, mediaPersisted.Name);
            Assert.AreEqual(media.Path, mediaPersisted.Path);
            Assert.AreEqual(media.Type, mediaPersisted.Type);
        }
    }
}
