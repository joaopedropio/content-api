using System;
using System.Collections.Generic;

namespace ContentApi.Domain.Entities
{
    public class Content
    {
        public uint? Id { get; set; }

        public string Name { get; set; }

        public Media CoverImage { get; set; }

        public IList<KeyValuePair<Person, Ocupation>> Professionals { get; set; }

        public string Country { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Studio { get; set; }
    }
}
