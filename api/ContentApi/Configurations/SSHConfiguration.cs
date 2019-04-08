namespace ContentApi.Configurations
{
    public class SSHConfiguration
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; } = 22;
    }
}
