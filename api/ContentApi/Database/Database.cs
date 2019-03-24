using MySql.Data.MySqlClient;
using System.Threading;
using System;
using Dapper;

namespace ContentApi.Database
{
    public static class DatabaseSetup
    {
        public static void InitiateTable(string connectionString)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = @"

                    /* MEDIAS */

                    CREATE TABLE IF NOT EXISTS MEDIAS (
                        ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                        NAME VARCHAR(255),
                        DESCRIPTION VARCHAR(255),
                        PATH VARCHAR(255),
                        TYPE INT UNSIGNED
                    );

                    /* MOVIES */

                    CREATE TABLE IF NOT EXISTS MOVIES (
                        ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                        NAME VARCHAR(255),
                        SHORT_DESCRIPTION VARCHAR(511),
                        SYNOPSIS VARCHAR(1023),
                        COVER_IMAGE_ID INT UNSIGNED,
                        BUDGET BIGINT UNSIGNED,
                        COUNTRY VARCHAR(255),
                        DURATION_SEC BIGINT UNSIGNED,
                        STUDIO VARCHAR(255),
                        RELEASE_DATE DATETIME,
                        VIDEO_ID INT UNSIGNED,
                        FOREIGN KEY (COVER_IMAGE_ID) REFERENCES MEDIAS(ID),
                        FOREIGN KEY (VIDEO_ID) REFERENCES MEDIAS(ID)
                    );

                    /* PERSONS */

                    CREATE TABLE IF NOT EXISTS PERSONS (
                        ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                        NAME VARCHAR(255),
                        BIRTHDAY DATETIME,
                        NATIONALITY VARCHAR(255)
                    );

                    /* CONTENT_TYPE */

                    /*
                    CREATE TABLE IF NOT EXISTS CONTENT_TYPE (
                        ID INT UNSIGNED PRIMARY KEY,
                        TYPE VARCHAR(100)
                    );

                    INSERT INTO CONTENT_TYPE (ID, TYPE)
                    SELECT * FROM (SELECT 0, 'Movie') AS TMP
                    WHERE NOT EXISTS (
                        SELECT TYPE FROM CONTENT_TYPE WHERE TYPE = 'Movie'
                    ) LIMIT 1;

                    INSERT INTO CONTENT_TYPE (ID, TYPE)
                    SELECT * FROM (SELECT 1, 'Serie') AS TMP
                    WHERE NOT EXISTS (
                        SELECT TYPE FROM CONTENT_TYPE WHERE TYPE = 'Serie'
                    ) LIMIT 1;
                    */

                    /* OCUPATIONS */

                    CREATE TABLE IF NOT EXISTS OCUPATIONS (
                        ID INT UNSIGNED PRIMARY KEY,
                        OCUPATION_NAME VARCHAR(100)
                    );

                    INSERT INTO OCUPATIONS (ID, OCUPATION_NAME)
                    SELECT * FROM (SELECT 0, 'Actor') AS TMP
                    WHERE NOT EXISTS (
                        SELECT OCUPATION_NAME FROM OCUPATIONS WHERE OCUPATION_NAME = 'Actor'
                    ) LIMIT 1;

                    INSERT INTO OCUPATIONS (ID, OCUPATION_NAME)
                    SELECT * FROM (SELECT 1, 'Director') AS TMP
                    WHERE NOT EXISTS (
                        SELECT OCUPATION_NAME FROM OCUPATIONS WHERE OCUPATION_NAME = 'Director'
                    ) LIMIT 1;

                    INSERT INTO OCUPATIONS (ID, OCUPATION_NAME)
                    SELECT * FROM (SELECT 2, 'Producer') AS TMP
                    WHERE NOT EXISTS (
                        SELECT OCUPATION_NAME FROM OCUPATIONS WHERE OCUPATION_NAME = 'Producer'
                    ) LIMIT 1;

                    INSERT INTO OCUPATIONS (ID, OCUPATION_NAME)
                    SELECT * FROM (SELECT 3, 'Music') AS TMP
                    WHERE NOT EXISTS (
                        SELECT OCUPATION_NAME FROM OCUPATIONS WHERE OCUPATION_NAME = 'Music'
                    ) LIMIT 1;

                    INSERT INTO OCUPATIONS (ID, OCUPATION_NAME)
                    SELECT * FROM (SELECT 4, 'Editor') AS TMP
                    WHERE NOT EXISTS (
                        SELECT OCUPATION_NAME FROM OCUPATIONS WHERE OCUPATION_NAME = 'Editor'
                    ) LIMIT 1;

                    /* PROFESSIONALS */

                    CREATE TABLE IF NOT EXISTS PROFESSIONALS (
                        CONTENT_ID INT UNSIGNED,
                        PERSON_ID INT UNSIGNED,
                        OCUPATION_ID INT UNSIGNED,
                        PRIMARY KEY (CONTENT_ID, PERSON_ID, OCUPATION_ID)
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
