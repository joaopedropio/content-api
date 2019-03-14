using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;

namespace ContentApi.Domain
{
    public class Content
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonIgnore]
        public Stream CoverImage { get; set; }

        //[BsonElement("Images")]
        //public IList<Stream> Images { get; set; }

        [BsonElement("Country")]
        public string Country { get; set; }

        //[BsonElement("ReleaseDate")]
        //public DateTime ReleaseDate { get; set; }

        [BsonElement("Studio")]
        public string Studio { get; set; }
    }
}
