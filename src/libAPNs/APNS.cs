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
    using System.Security.Cryptography.X509Certificates;
    using Connection;
    using Notifications;

    public class APNS : IAPNS
    {
        private readonly X509Certificate2 certificate;

        private readonly IAPNSConnection connection;
        private readonly bool useSandbox;

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

        public IErrorResponse SendEnhancedNotification(IEnhancedNotification enhancedNotification)
        {
            this.connection.Connect();
            this.connection.Write(enhancedNotification.ToByteArray());
            var response = new byte[6];
            this.connection.Read(response, 0, response.Length);
            this.connection.Disconnect();
            IErrorResponse errorResponse = null;

            if (response[0] == 0x08)
            {
                // we received an error response...
                errorResponse = new ErrorResponse(BitConverter.ToInt32(response, 2), (ErrorResponseStatusCode) response[1]);
            }

            return errorResponse;
        }

        public void SendSimpleNotification(ISimpleNotification simpleNotification)
        {
            this.connection.Connect();
            this.connection.Write(simpleNotification.ToByteArray());
            this.connection.Disconnect();
        }
    }
}