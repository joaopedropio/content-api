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
        public MovieRepository(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
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

                return conn.Query<dynamic>(query)
                    .Select(m => new Movie()
                    {
                        Id = m.ID,
                        Name = m.NAME,
                        Budget = m.BUDGET,
                        Country = m.COUNTRY,
                        CoverImage = m.COVER_IMAGE,
                        Duration = m.DURATION_SEC,
                        ReleaseDate = m.RELEASE_DATE,
                        ShortDescription = m.SHORT_DESCRIPTION,
                        Studio = m.STUDIO,
                        Synopsis = m.SYNOPSIS
                    })
                    .First();
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
                var query = $"INSERT INTO MOVIES (NAME, SHORT_DESCRIPTION, SYNOPSIS, BUDGET, COUNTRY, COVER_IMAGE_PATH, DURATION_SEC, RELEASE_DATE, STUDIO, VIDEO_PATH) " +
                    $"VALUES ('{movie.Name}', '{movie.ShortDescription}', '{movie.Synopsis}', "
                    + $"'{movie.Budget}', '{movie.Country}', '{movie.CoverImage.Id}', '{movie.Duration}', "
                    + $"'{movie.ReleaseDate}', '{movie.Studio}'), '{movie.Video.Id}'";
                var affectedrows = conn.Execute(query, movie);
                if (affectedrows == 0)
                    throw new Exception("Error while inserting movie");

                return conn.QueryFirstOrDefault<int>("SELECT LAST_INSERT_ID()");
            }
        }
    }
}
