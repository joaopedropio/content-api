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
    public class PersonRepository : IRepository<Person>
    {
        private string connectionString;

        public PersonRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Delete(uint id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"DELETE FROM PERSONS WHERE ID = {id}";
                var affectedrows = conn.Execute(query);
                if (affectedrows == 0)
                    throw new Exception($"Error while deleting Person. Id: {id}");
            }
        }

        public IList<Person> Get()
        {
            throw new NotImplementedException();
        }

        public Person Get(uint id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"SELECT * FROM PERSONS WHERE ID = '{id}'";

                var queryResult = conn.Query<dynamic>(query);

                return Parse(queryResult).First();
            }
        }

        public IEnumerable<uint> InsertMany(IEnumerable<Person> persons)
        {
            return persons.Select(p => Insert(p));
        }

        public uint Insert(Person person)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = QueryHelper.CreateInsertQuery("PERSONS", new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("NAME", person.Name),
                    new KeyValuePair<string, string>("BIRTHDAY", person.Birthday.ToString("yyyy-MM-dd HH:mm:ss")),
                    new KeyValuePair<string, string>("NATIONALITY", person.Nationality)
                });

                var affectedrows = conn.Execute(query);
                if (affectedrows == 0)
                    throw new Exception("Error while inserting person");

                return conn.QueryFirstOrDefault<uint>("SELECT LAST_INSERT_ID()");
            }
        }

        public IEnumerable<Person> Parse(IEnumerable<dynamic> queryResult)
        {
            return queryResult.Select(m =>
            {
                var person = new Person();
                person.Id = m.ID;
                person.Name = m.NAME;
                person.Birthday = m.BIRTHDAY;
                person.Nationality = m.NATIONALITY;
                return person;
            });
        }
    }
}
