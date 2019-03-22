using ContentApi.Domain;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentApi.Database
{
    public class MovieRepository
    {
        private string connectionString;
        public MovieRepository(string databaseConnectionString)
        {
            this.connectionString = databaseConnectionString;
            Database.Bootstrap(connectionString);
        }

        public List<Movie> Get()
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
                        CoverImagePath = m.COVER_IMAGE_PATH,
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
                var query = $"INSERT INTO MOVIES (NAME, SHORT_DESCRIPTION, SYNOPSIS, BUDGET, COUNTRY, COVER_IMAGE_PATH, DURATION_SEC, RELEASE_DATE, STUDIO) " +
                    $"VALUES ('{movie.Name}', '{movie.ShortDescription}', '{movie.Synopsis}', "
                    + $"'{movie.Budget}', '{movie.Country}', '{movie.CoverImagePath}', '{movie.Duration}', "
                    + $"'{movie.ReleaseDate}', '{movie.Studio}')";
                var affectedrows = conn.Execute(query, movie);
                if (affectedrows == 0)
                    throw new Exception("Error while inserting movie");

                return conn.QueryFirstOrDefault<int>("SELECT LAST_INSERT_ID()");
            }
        }
    }
}
