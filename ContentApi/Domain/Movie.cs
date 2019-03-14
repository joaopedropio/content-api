using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContentApi.Domain
{
    public class Movie : Content
    {

        [BsonElement("Synopsis")]
        public string Synopsis { get; set; }

        [BsonElement("ShortDescription")]
        public string ShortDescription { get; set; }

        //[BsonElement("Professionals")]
        //public IList<IDictionary<Person, Ocupation>> Professionals { get; set; }

        [BsonElement("Duration")]
        public int Duration { get; set; }

        [BsonElement("Budget")]
        public long Budget { get; set; }

        //[BsonElement("Media")]
        //public Media Media { get; set; }
    }
}
