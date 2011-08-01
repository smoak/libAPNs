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
namespace libAPNs.Feedback
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Connection;

    public class FeedbackService : IFeedbackService
    {
        private IAPNSConnection connection;
        private bool useSandbox;
        private X509Certificate2 certificate;

        public FeedbackService(bool useSandbox, X509Certificate2 certificate)
        {
            this.useSandbox = useSandbox;
            this.certificate = certificate;

            if (this.useSandbox)
            {
                this.connection = new SandboxFeedbackConnection(this.certificate);
            }
            else
            {
                this.connection = new GatewayFeedbackConnection(this.certificate);
            }
        }

        public IList<IFeedbackTuple> GetFailedDeliveryAttempts()
        {
            this.connection.Connect();
            var results = new List<IFeedbackTuple>();
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = this.connection.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                buffer = ms.ToArray();
                var curPos = 0;
                while (curPos + 38 < buffer.Length)
                {
                    results.Add(this.ParseFromByteArray(buffer.Skip(curPos).Take(38).ToArray()));
                    curPos += 38;
                }

            }

            return results;
        }

        private IFeedbackTuple ParseFromByteArray(byte[] bytes)
        {
            int offset = 0;
            // first four bytes
            var time_t = BitConverter.ToInt32(bytes, offset);
            offset += 4;
            // second two bytes
            var tokenLen = BitConverter.ToInt16(bytes, offset);
            offset += 2;
            // remaning is the token in binary form
            var tokenBytes = bytes.Skip(offset).Take(tokenLen).ToArray();

            return new FeedbackTuple(DateTime.FromBinary(time_t), DeviceToken.FromBinary(tokenBytes));
        }
    }
}