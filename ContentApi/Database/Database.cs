using MySql.Data.MySqlClient;
using System.Threading;
using System;
using Dapper;

namespace ContentApi.Database
{
    public static class Database
    {
        public static void InitiateTable(string connectionString)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"
                    CREATE TABLE IF NOT EXISTS MOVIES (
                        ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                        NAME VARCHAR(255),
                        SHORT_DESCRIPTION VARCHAR(511),
                        SYNOPSIS VARCHAR(1023),
                        COVER_IMAGE_PATH VARCHAR(255),
                        BUDGET BIGINT UNSIGNED,
                        COUNTRY VARCHAR(255),
                        DURATION_SEC BIGINT UNSIGNED,
                        STUDIO VARCHAR(255),
                        RELEASE_DATE VARCHAR(10)
                    );
                ";

                var command = connection.CreateCommand();
                connection.Open();
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }

        public static void Bootstrap(string connectionString, int timeOut = 30000)
        {
            var time = 1000;
            Thread.Sleep(time);
            try
            {
                InitiateTable(connectionString);
            }
            catch (System.Exception ex)
            {
                if (timeOut < 0) throw ex;

                Console.WriteLine(ex.Message);
                Console.WriteLine(timeOut / time + " tries left...");
                Bootstrap(connectionString, timeOut - time);
            }
        }
    }
}
