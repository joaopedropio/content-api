namespace ContentApi.Domain
{
    public class Media
    {
        public string Name { get; set; }
        public MediaType MediaType { get; set; }
        public File Manifest { get; set; }
    }
}
