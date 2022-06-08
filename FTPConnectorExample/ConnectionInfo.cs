namespace FTPConnectorExample
{
    public class ConnectionInfo
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ProxyType { get; set; }
        public string ProxyHostName { get; set; }
        public string ProxyPort { get; set; }
        public string ProxyUser { get; set; }
        public string ProxyPassword { get; set; }
        public string SshKey { get; set; }
    }
}
