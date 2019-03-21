using ContentApi.Domain;
using Dapper;
using MySql.Data.MySqlClient;
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
                return conn.QueryFirstOrDefault<Movie>(query);
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

        public bool Insert(Movie movie)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = "INSERT INTO MOVIES (NAME, SHORT_DESCRIPTION, SYNOPSIS)" +
                    $"VALUES ('{movie.Name}', '{movie.ShortDescription}', '{movie.Synopsis}')";
                var affectedrows = conn.Execute(query, movie);
                return affectedrows > 0;
            }
        }
    }
}
