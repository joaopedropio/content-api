namespace ContentApi.Domain.Entities
{
    public class Episode
    {
        public string Name { get; set; }
        public int EpisodeNumber { get; set; }
        public int SeasonNumber { get; set; }
        public string ShortDescription { get; set; }
        public int Duration { get; set; }
        public long Budget { get; set; }
        public string VideoPath { get; set; }
    }
}
