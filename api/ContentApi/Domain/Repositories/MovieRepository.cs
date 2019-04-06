using ContentApi.Database;
using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentApi.Domain.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private string connectionString;
        private MediaRepository mediaRepository;
        private PersonRepository personRepository;

        public MovieRepository(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
            this.mediaRepository = new MediaRepository(connectionString);
            this.personRepository = new PersonRepository(connectionString);
        }

        public IList<Movie> Get()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = "SELECT ID FROM MOVIES";
                var moviesIds = conn.Query<uint>(query).ToList();

                return moviesIds.Select(id => Get(id)).ToList();
            }
        }

        public Movie Get(uint id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"SELECT * FROM MOVIES WHERE ID = '{id}'";

                var queryResult = conn.Query<dynamic>(query);

                if (queryResult.Count() == 0)
                    return null;

                var movie = Parse(queryResult).First();

                var professionals = GetProfessionalsByContentId(id);

                movie.Professionals = professionals;

                return movie;
            }
        }

        public void Delete(uint id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"DELETE FROM MOVIES WHERE ID = '{id}'";
                var affectedrows = conn.Execute(query);
                if (affectedrows == 0)
                    throw new ArgumentException($"There is no Movie to delete. Id: {id}");
            }
        }

        public uint Insert(Movie movie)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = QueryHelper.CreateInsertQuery("MOVIES", new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("NAME", movie.Name),
                    new KeyValuePair<string, string>("SHORT_DESCRIPTION", movie.ShortDescription),
                    new KeyValuePair<string, string>("SYNOPSIS", movie.Synopsis),
                    new KeyValuePair<string, string>("BUDGET", movie.Budget.ToString()),
                    new KeyValuePair<string, string>("COUNTRY", movie.Country),
                    new KeyValuePair<string, string>("DURATION_SEC", movie.Duration.ToString()),
                    new KeyValuePair<string, string>("RELEASE_DATE", movie.ReleaseDate.ToString("yyyy-MM-dd HH:mm:ss")),
                    new KeyValuePair<string, string>("STUDIO", movie.Studio),
                    new KeyValuePair<string, string>("VIDEO_ID", movie.Video.Id.ToString()),
                    new KeyValuePair<string, string>("COVER_IMAGE_ID", movie.CoverImage.Id.ToString())
                });

                var affectedrows = conn.Execute(query);
                if (affectedrows == 0)
                    throw new Exception("Error while inserting movie");

                var movieId = conn.QueryFirstOrDefault<uint>("SELECT LAST_INSERT_ID()");

                InsertMany(movieId, movie.Professionals);

                return movieId;
            }
        }

        private IList<Professional> GetProfessionalsByContentId(uint contentId)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var selectProfessionals = $"SELECT * FROM PROFESSIONALS WHERE CONTENT_ID = '{contentId}'";


                var professionals = conn.Query<dynamic>(selectProfessionals);

                if (professionals.Count() == 0)
                    return new List<Professional>();

                return ParseProfessionals(professionals).ToList();
            }
        }

        private IList<uint> InsertMany(uint contentId, IList<Professional> professionals)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var professionalsQueryColumns = professionals.Select(pro =>
                {
                    return new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("CONTENT_ID", contentId.ToString()),
                            new KeyValuePair<string, string>("PERSON_ID", pro.Person.Id.ToString()),
                            new KeyValuePair<string, string>("OCUPATION_ID", pro.Ocupation.GetHashCode().ToString())
                        };
                });

                var insertProfessionalsQuery = professionalsQueryColumns.Select(q => QueryHelper.CreateInsertQuery("PROFESSIONALS", q));
                return insertProfessionalsQuery.Select(q =>
                {
                    var affectedrows = conn.Execute(q);
                    if (affectedrows == 0)
                        throw new Exception("Error while inserting professional");

                    return conn.QueryFirstOrDefault<uint>("SELECT LAST_INSERT_ID()");

                }).ToList();
            }
        }

        public IEnumerable<Professional> ParseProfessionals(IEnumerable<dynamic> queryResult)
        {
            return queryResult.Select(p =>
            {
                var professional = new Professional();
                professional.Person = this.personRepository.Get(p.PERSON_ID);
                professional.Ocupation = Enum.Parse<Ocupation>(p.OCUPATION_ID.ToString());

                return professional;
            });
        }

        public IEnumerable<Movie> Parse(IEnumerable<dynamic> queryResult)
        {
            return queryResult.Select(m => new Movie()
            {
                Id = m.ID,
                Name = m.NAME,
                Budget = m.BUDGET,
                Country = m.COUNTRY,
                Duration = m.DURATION_SEC,
                ReleaseDate = m.RELEASE_DATE,
                ShortDescription = m.SHORT_DESCRIPTION,
                Studio = m.STUDIO,
                Synopsis = m.SYNOPSIS,
                CoverImage = this.mediaRepository.Get(m.COVER_IMAGE_ID),
                Video = this.mediaRepository.Get(m.VIDEO_ID)
            });
        }
    }
}
