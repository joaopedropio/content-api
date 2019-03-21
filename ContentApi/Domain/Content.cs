using System;
using System.Collections.Generic;

namespace ContentApi.Domain
{
    public class Content
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CoverImagePath { get; set; }

        //public IList<string> ImagesPaths { get; set; }

        public string Country { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Studio { get; set; }
    }
}
