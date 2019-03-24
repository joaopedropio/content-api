using System;
using System.Collections.Generic;
using System.Linq;
using ContentApi.Database;
using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace ContentApi.Domain.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private string connectionString;

        public MediaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Media> Get()
        {
            throw new System.NotImplementedException();
        }

        public Media Get(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"SELECT * FROM MEDIAS WHERE ID = '{id}'";

                return conn.Query<dynamic>(query).Select(m =>
                {
                    var mediaType = Enum.Parse<MediaType>(m.TYPE.ToString());

                    var media = new Media();
                    media.Id = m.ID;
                    media.Description = m.DESCRIPTION;
                    media.Name = m.NAME;
                    media.Path = m.PATH;
                    media.Type = mediaType;
                    return media;
                }).First();
            }
        }

        public int Insert(Media media)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var columns = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("NAME", media.Name),
                    new KeyValuePair<string, string>("DESCRIPTION", media.Description),
                    new KeyValuePair<string, string>("PATH", media.Path),
                    new KeyValuePair<string, string>("TYPE", media.Type.GetHashCode().ToString()),
                };
                var query = QueryHelper.CreateInsertQuery("MEDIAS", columns);
                var affectedrows = conn.Execute(query);
                if (affectedrows == 0)
                    throw new Exception("Error while inserting person");

                return conn.QueryFirstOrDefault<int>("SELECT LAST_INSERT_ID()");
            }
        }
    }
}
