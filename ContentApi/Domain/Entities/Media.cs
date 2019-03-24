namespace ContentApi.Domain.Entities
{
    public class Media
    {
        public uint? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public MediaType? Type { get; set; }
    }
}
