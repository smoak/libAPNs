// -----------------------------------------------------------------------
// <copyright file="APNCertificateException.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Security;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class APNSCertificateException : Exception
    {
        private SslPolicyErrors sslPolicyErrors;

        public APNSCertificateException(string message)
            : base(message)
        {

        }

        public APNSCertificateException(string message, Exception exception)
            : base(message, exception)
        {

        }

        public APNSCertificateException(SslPolicyErrors sslPolicyErrors)
            : base(sslPolicyErrors.ToString())
        {
            this.sslPolicyErrors = sslPolicyErrors;
        }
    }
}
