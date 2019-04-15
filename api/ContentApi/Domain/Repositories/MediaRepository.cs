using System;
using System.Collections.Generic;
using System.Linq;
using ContentApi.Database;
using ContentApi.Domain.Entities;
using ContentApi.Domain.Repositories.Interfaces;
using ContentApi.Helpers;
using Dapper;
using MySql.Data.MySqlClient;

namespace ContentApi.Domain.Repositories
{
    public class MediaRepository : IRepository<Media>
    {
        private string connectionString;

        public MediaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Delete(uint id)
        {
            using(var conn = new MySqlConnection(connectionString))
            {
                var query = $"DELETE FROM MEDIAS WHERE ID = {id}";
                var affectedrows = conn.Execute(query);
                if (affectedrows == 0)
                    throw new ArgumentException($"Error while deleting media. Id: {id}");
            }
        }

        public IList<Media> Get()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"SELECT * FROM MEDIAS";

                var medias = conn.Query<dynamic>(query);

                if (medias == null || medias.Count() == 0)
                    return new List<Media>();

                return Parse(medias).ToList();
            }
        }

        public IEnumerable<Media> Parse(IEnumerable<dynamic> queryResult)
        {
            return queryResult.Select(m =>
            {
                var mediaType = Enum.Parse<MediaType>(m.TYPE.ToString());

                var media = new Media();
                media.Id = m.ID;
                media.Description = m.DESCRIPTION;
                media.Name = m.NAME;
                media.Path = m.PATH;
                media.Type = mediaType;
                return media;
            });
        }

        public Media Get(uint id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                var query = $"SELECT * FROM MEDIAS WHERE ID = '{id}'";

                var medias = conn.Query<dynamic>(query);

                if (medias.Count() == 0)
                    return null;

                return Parse(medias).First();
            }
        }

        public uint Insert(Media media)
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

                return conn.QueryFirstOrDefault<uint>("SELECT LAST_INSERT_ID()");
            }
        }

        public IList<Media> GetByName(string name)
        {
            var query = QueryHelper.CreateSearchBy("MEDIAS", "NAME", name);
            using (var conn = new MySqlConnection(connectionString))
            {
                var ids = conn.Query<uint>(query);
                if (ids.Count() == 0)
                    return new List<Media>();

                return ids.Select(id => this.Get(id)).ToList();
            }
        }
    }
}
