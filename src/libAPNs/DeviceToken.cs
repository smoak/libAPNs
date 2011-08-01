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
namespace libAPNs
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public struct DeviceToken
    {
        private readonly string tokenData;

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
            get { return this.ToByteArray().Length; }
        }

        /// <summary>
        /// Converts the device token to its binary representation
        /// </summary>
        /// <returns>
        /// The binary data of the device token
        /// </returns>
        public byte[] ToByteArray()
        {
            var token = this.tokenData.ToUpper();
            if (token.Length%2 == 1)
            {
                token = '0' + token;
            }

            var data = new byte[token.Length/2];

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = Convert.ToByte(token.Substring(i*2, 2), 16);
            }

            return data;
        }
    }
}