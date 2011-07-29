// -----------------------------------------------------------------------
// <copyright file="APNS.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Connection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class APNS : IAPNS
    {
        private bool useSandbox;

        private X509Certificate2 certificate;

        private IAPNSConnection connection;

        public APNS(bool useSandbox, X509Certificate2 certificate)
        {
            this.useSandbox = useSandbox;
            this.certificate = certificate;

            if (this.useSandbox)
            {
                this.connection = new SandboxGatewayConnection(this.certificate);
            }
            else
            {
                this.connection = new GatewayConnection(this.certificate);
            }
        }

        public void SendSimpleNotification(ISimpleNotification simpleNotification)
        {
            this.connection.Connect();
            this.connection.Write(simpleNotification.ToByteArray());
            this.connection.Disconnect();
        }
    }
}
