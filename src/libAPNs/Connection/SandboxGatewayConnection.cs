// -----------------------------------------------------------------------
// <copyright file="SandboxGateway.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs.Connection
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class SandboxGatewayConnection : APNSConnection
    {
        internal const string SANDBOX_HOST = "gateway.sandbox.push.apple.com";

        public SandboxGatewayConnection(X509Certificate2 certificate)
            : base(certificate)
        {
            this.host = SANDBOX_HOST;
        }
    }
}
