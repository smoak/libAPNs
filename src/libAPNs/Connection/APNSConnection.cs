// -----------------------------------------------------------------------
// <copyright file="APNSConnection.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs.Connection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Exceptions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal abstract class APNSConnection : IAPNSConnection
    {
        protected X509Certificate2 certificate;

        private TcpClient tcpClient;

        private SslStream sslStream;

        protected string host;

        // same port for sandbox and production
        internal const int PORT = 2195;

        public APNSConnection(X509Certificate2 certificate)
        {
            this.certificate = certificate;
        }

        public void Connect()
        {
            this.tcpClient = new TcpClient(this.host, PORT);
            this.sslStream = new SslStream(
                            this.tcpClient.GetStream(),
                            false,
                            ValidateServerCertificate,
                            null);
            var certificatesCollection = new X509Certificate2Collection(this.certificate);
            this.sslStream.AuthenticateAsClient(this.host, certificatesCollection, SslProtocols.Tls, false);
        }

        public void Disconnect()
        {
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
            }
        }

        public void Write(byte[] data)
        {
            if (this.tcpClient == null)
            {
                throw new ApplicationException("tcpClient hasn't been initialized. Perhaps you forgot to call Connect()?");
            }

            this.sslStream.Write(data);
            
        }

        public void Write(byte[] data, int offset, int count)
        {
            if (this.tcpClient == null)
            {
                throw new ApplicationException("tcpClient hasn't been initialized. Perhaps you forgot to call Connect()?");
            }

            this.sslStream.Write(data, offset, count);
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            throw new APNSCertificateException(sslPolicyErrors);
        }
    }
}
