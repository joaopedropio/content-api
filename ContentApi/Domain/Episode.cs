using System.Collections.Generic;

namespace ContentApi.Domain
{
    public class Episode
    {
        public string Name { get; set; }
        public int EpisodeNumber { get; set; }
        public int SeasonNumber { get; set; }
        public string ShortDescription { get; set; }
        public IList<KeyValuePair<Person, Ocupation>> Professionals { get; set; }
        public int Duration { get; set; }
        public long Budget { get; set; }
        public Media Media { get; set; }
    }
}
