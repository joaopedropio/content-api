using System;
using System.Collections.Generic;
using System.Text;

namespace ContentClient.Models
{
    public class Movie
    {
        public string Synopsis { get; set; }

        public string ShortDescription { get; set; }

        public ulong Duration { get; set; }

        public ulong Budget { get; set; }

        public Media Video { get; set; }

        public IList<Professional> Professionals { get; set; }
    }
}
