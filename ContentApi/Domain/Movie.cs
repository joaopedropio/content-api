namespace ContentApi.Domain
{
    public class Movie : Content
    {
        public string Synopsis { get; set; }

        public string ShortDescription { get; set; }

        //public IList<IDictionary<Person, Ocupation>> Professionals { get; set; }

        public ulong Duration { get; set; }

        public ulong Budget { get; set; }

        //public Media Media { get; set; }
    }
}
