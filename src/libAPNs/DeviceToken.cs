// -----------------------------------------------------------------------
// <copyright file="DeviceToken.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public struct DeviceToken
    {
        private string tokenData;

        /// <summary>
        /// Represents a DeviceIdentifier to send push messages to.
        /// </summary>
        /// <param name="tokenData">The DeviceIdentifier with no spaces or special characters. Just letters and numbers.</param>
        public DeviceToken(string tokenData)
        {
            if (tokenData == null)
            {
                throw new ArgumentNullException("tokenData");
            }

            if (!Regex.IsMatch(tokenData, "^[A-Za-z0-9]+$"))
            {
                throw new ArgumentException("tokenData contains invalid characters");
            }

            this.tokenData = tokenData;
        }

        public int Length
        {
            get
            {
                return this.ToByteArray().Length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public byte[] ToByteArray()
        {
            var token = this.tokenData.ToUpper();
            if (token.Length % 2 == 1)
            {
                token = '0' + token;
            }

            var data = new byte[token.Length / 2];

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = Convert.ToByte(token.Substring(i * 2, 2), 16);
            }

            return data;
        }

    }
}
