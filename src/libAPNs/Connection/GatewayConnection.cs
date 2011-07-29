// -----------------------------------------------------------------------
// <copyright file="GatewayConnection.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs.Connection
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class GatewayConnection : APNSConnection
    {
        internal const string PRODUCTION_HOST = "gateway.push.apple.com";

        public GatewayConnection(X509Certificate2 certificate)
            : base(certificate)
        {
            this.host = PRODUCTION_HOST;
        }
    }
}
