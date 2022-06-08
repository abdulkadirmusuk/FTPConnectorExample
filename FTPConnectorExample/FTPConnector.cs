using System;
using WinSCP;

namespace FTPConnectorExample
{
    public class FTPConnector : IFileAdapter
    {
        private const string SOCKS5_PROXY_METHOD = "2";
        private const string HTTP_PROXY_METHOD = "3";
        private readonly ConnectionInfo connectionInfo;
        
        public FTPConnector(ConnectionInfo connectionInfo)
        {
            this.connectionInfo = connectionInfo;
        }

        /// <summary>
        /// Connect FTP server with connection info
        /// </summary>
        /// <returns></returns>
        public string Connect()
        {
            try
            {
                // Setup session options 
                SessionOptions sessionOptions = new SessionOptions {
                    Protocol = Protocol.Ftp,
                    HostName = connectionInfo.HostName
                };
                if (connectionInfo.Port != 0) {
                    sessionOptions.PortNumber = connectionInfo.Port;
                }
                if (!string.IsNullOrWhiteSpace(connectionInfo.UserName)) {
                    sessionOptions.UserName = connectionInfo.UserName;
                }
                if (!string.IsNullOrWhiteSpace(connectionInfo.Password)) {
                    sessionOptions.Password = connectionInfo.Password;
                }

                //Proxy configurations
                string proxyType = connectionInfo.ProxyType == 0 ? HTTP_PROXY_METHOD : SOCKS5_PROXY_METHOD;
                sessionOptions.AddRawSettings("ProxyMethod", proxyType);
                sessionOptions.AddRawSettings("ProxyHost", connectionInfo.ProxyHostName);
                sessionOptions.AddRawSettings("ProxyPort", connectionInfo.ProxyPort);

                if (!string.IsNullOrWhiteSpace(connectionInfo.ProxyUser)) {
                    sessionOptions.AddRawSettings("ProxyUsername", connectionInfo.ProxyUser);
                }
                if (!string.IsNullOrWhiteSpace(connectionInfo.ProxyPassword)) {
                    sessionOptions.AddRawSettings("ProxyPassword", connectionInfo.ProxyPassword);
                }

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);
                    if (!session.Opened) {
                        throw new Exception("Error occured");
                    }
                }

                return "Connection Successfully";
            }
            catch (Exception e)
            {
                return e.Message + "\n\n"+e.StackTrace.ToString();
            }
        }
    }
}
