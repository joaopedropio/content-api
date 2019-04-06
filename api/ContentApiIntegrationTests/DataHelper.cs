using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories;
using ContentApi.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentApiIntegrationTests
{
    public class DataHelper
    {
        private MovieRepository movieRepository;
        private MediaRepository mediaRepository;
        private PersonRepository personRepository;

        public DataHelper(string connectionString)
        {
            this.movieRepository = new MovieRepository(connectionString);
            this.mediaRepository = new MediaRepository(connectionString);
            this.personRepository = new PersonRepository(connectionString);
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

        public Person GetSamplePerson()
        {
            return new Person()
            {
                Name = "Arnold Schwarzenegger",
                Birthday = new DateTime(1947, 7, 30),
                Nationality = "Austria"
            };
        }

        public List<Professional> GetSampleProfessionals()
        {
            var professional1 = new Professional()
            {
                Person = new Person()
                {
                    Name = "Arnold Schwarzenegger",
                    Birthday = new DateTime(1947, 7, 30),
                    Nationality = "Austria"
                },
                Ocupation = Ocupation.Actor
            };

            var professional2 = new Professional()
            {
                Person = new Person()
                {
                    Name = "Linda Hamilton",
                    Birthday = new DateTime(1956, 9, 26),
                    Nationality = "United States of America"
                },
                Ocupation = Ocupation.Actor
            };


            return new List<Professional>()
            {
                professional1,
                professional2
            };
        }

        public Media GetSampleCoverImage()
        {
            return new Media()
            {
                Description = "Cover image do Piratas do Caribe",
                Name = "coverimagepiratas",
                Path = "/imges/coverimagepiratas.png",
                Type = MediaType.Image
            };
        }

        public Media GetSampleVideo()
        {
            return new Media()
            {
                Description = "Video do Piratas do Caribe",
                Name = "videopiratas",
                Path = "/videos/videodopiratas.mpd",
                Type = MediaType.Video
            };
        }

        public Movie GetSampleMovie()
        {
            var coverImageSample = GetSampleCoverImage();
            var coverImageId = this.mediaRepository.Insert(coverImageSample);
            coverImageSample.Id = coverImageId;

            var videoSample = GetSampleVideo();
            var videoId = this.mediaRepository.Insert(videoSample);
            videoSample.Id = videoId;

            var professionalsSample = GetSampleProfessionals();

            var persons = professionalsSample.Select(pro => pro.Person);

            var personsIds = this.personRepository.InsertMany(persons).ToList();

            professionalsSample[0].Person.Id = personsIds[0];
            professionalsSample[1].Person.Id = personsIds[1];

            return new Movie()
            {
                Professionals = professionalsSample,
                CoverImage = coverImageSample,
                Video = videoSample,
                Budget = 123000000,
                Country = "EUA",
                Duration = 123123,
                Name = "Piratas do Caribe",
                ShortDescription = "Uns piratas ai muito loucos",
                Studio = "Disney",
                Synopsis = "Jack Sparrow tava fazendo umas baguncinhas no Caribe quando apareceu uma aventura do barulho",
                ReleaseDate = DateTime.Parse("01-01-2001 00:00:00")
            };
        }

        public void DeleteAll<T>(IRepository<T> repository) where T : IStorable, new()
        {
            var contents = repository.Get();

            foreach (var content in contents)
            {
                repository.Delete(content.Id.Value);
            }
        }
    }
}
