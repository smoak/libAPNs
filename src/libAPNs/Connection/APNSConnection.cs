// Copyright (C) <2011> by <Scott Moak>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
namespace libAPNs.Connection
{
    using System;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using Exceptions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal abstract class APNSConnection : IAPNSConnection
    {
        internal const int PORT = 2195;
        protected X509Certificate2 certificate;

        protected string host;
        protected int port = PORT;
        private SslStream sslStream;
        private TcpClient tcpClient;

        // same port for sandbox and production

        public APNSConnection(X509Certificate2 certificate)
        {
            this.certificate = certificate;
        }

        public bool CanRead
        {
            get { return this.sslStream.CanRead; }
        }

        public void Connect()
        {
            this.tcpClient = new TcpClient(this.host, this.port);
            this.sslStream = new SslStream(
                this.tcpClient.GetStream(),
                false,
                ValidateServerCertificate,
                null);
            var certificatesCollection = new X509Certificate2Collection(this.certificate);
            try
            {
                this.sslStream.AuthenticateAsClient(this.host, certificatesCollection, SslProtocols.Tls, false);

            }
            catch (AuthenticationException ex)
            {
                throw new NotificationException("Failed to authenticate", ex);
                
            }
            
            if (!this.sslStream.IsMutuallyAuthenticated)
            {
                throw new NotificationException("Failed to authenticate");
            }
        }

        public void Disconnect()
        {
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
            }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return this.sslStream.Read(buffer, offset, count);
        }

        public int ReadByte()
        {
            return this.sslStream.ReadByte();
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