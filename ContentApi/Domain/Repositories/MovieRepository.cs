using ContentApi.Database;
using ContentApi.Domain.Entities;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentApi.Domain.Repositories
{
    public class MovieRepository : IMovieRepository
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
                var query = "SELECT * FROM MOVIES";
                return conn.Query<Movie>(query).ToList();
            }
        }

        public Movie Get(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"SELECT * FROM MOVIES WHERE ID = '{id}'";

                var movie = conn.Query<dynamic>(query)
                    .Select(m => new Movie()
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
                    })
                    .First();

                var selectProfessionals = $"SELECT * FROM PROFESSIONALS WHERE CONTENT_ID = '{id}'";
                var professionals = conn.Query<dynamic>(selectProfessionals).Select(p =>
                {
                    var person = this.personRepository.Get(p.PERSON_ID);
                    var ocupation = Enum.Parse<Ocupation>(p.OCUPATION_ID.ToString());
                    return new KeyValuePair<Person, Ocupation>(person, ocupation);
                })
                .ToList();

                movie.Professionals = professionals;

                return movie;
            }
        }

        public bool Delete(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"DELETE FROM MOVIES WHERE ID = '{id}'";
                var affectedrows = conn.Execute(query);
                return affectedrows > 0;
            }
        }

        public int Insert(Movie movie)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                //var query = $"INSERT INTO MOVIES (NAME, SHORT_DESCRIPTION, SYNOPSIS, BUDGET, COUNTRY, COVER_IMAGE_PATH, DURATION_SEC, RELEASE_DATE, STUDIO, VIDEO_PATH) " +
                //    $"VALUES ('{movie.Name}', '{movie.ShortDescription}', '{movie.Synopsis}', "
                //    + $"'{movie.Budget}', '{movie.Country}', '{movie.CoverImage.Id}', '{movie.Duration}', "
                //    + $"'{movie.ReleaseDate}', '{movie.Studio}'), '{movie.Video.Id}'";

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
                /*
                    CONTENT_ID INT UNSIGNED,
                    PERSON_ID INT UNSIGNED,
                    OCUPATION_ID INT UNSIGNED,
                    PRIMARY KEY (CONTENT_ID, PERSON_ID, OCUPATION_ID)
                */

                var affectedrows = conn.Execute(query);
                if (affectedrows == 0)
                    throw new Exception("Error while inserting movie");

                var movieId = conn.QueryFirstOrDefault<int>("SELECT LAST_INSERT_ID()");

                var professionalsQueryColumns = movie.Professionals.Select(p =>
                {
                    return new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("CONTENT_ID", movieId.ToString()),
                        new KeyValuePair<string, string>("PERSON_ID", p.Key.Id.ToString()),
                        new KeyValuePair<string, string>("OCUPATION_ID", p.Value.GetHashCode().ToString())
                    };
                });

                var insertProfessionalsQuery = professionalsQueryColumns.Select(q => QueryHelper.CreateInsertQuery("PROFESSIONALS", q));
                insertProfessionalsQuery.Select(i => conn.Execute(i)).ToList();

                return movieId;
            }
        }
    }
}
