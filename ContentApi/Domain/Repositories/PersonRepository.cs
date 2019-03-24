using ContentApi.Domain.Entities;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentApi.Domain.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private string connectionString;

        public PersonRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Person> Get()
        {
            throw new NotImplementedException();
        }

        public Person Get(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"SELECT * FROM PERSONS WHERE ID = '{id}'";

                return conn.Query<dynamic>(query).Select(m =>
                {
                    var person = new Person();
                    person.Id = m.ID;
                    person.Name = m.NAME;
                    person.Birthday = m.BIRTHDAY;
                    person.Nationality = m.NATIONALITY;
                    return person;
                }).First();
            }
        }

        public int Insert(Person person)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"INSERT INTO PERSONS (NAME, BIRTHDAY, NATIONALITY) " +
                    $"VALUES ('{person.Name}', '{person.Birthday.ToString("yyyy-MM-dd HH:mm:ss")}', '{person.Nationality}');";
                var affectedrows = conn.Execute(query);
                if (affectedrows == 0)
                    throw new Exception("Error while inserting person");

                return conn.QueryFirstOrDefault<int>("SELECT LAST_INSERT_ID()");
            }
        }
    }
}
