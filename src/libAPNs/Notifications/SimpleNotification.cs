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
namespace libAPNs.Notifications
{
    using System.IO;
    using System.Net;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SimpleNotification : ISimpleNotification
    {
        internal const byte COMMAND_BYTE = 0;

        private readonly IPayload payload;

        private DeviceToken deviceToken;

        public SimpleNotification(DeviceToken token, IPayload payload)
        {
            this.deviceToken = token;
            this.payload = payload;
        }

        public DeviceToken DeviceToken
        {
            get { return this.deviceToken; }
        }

        public IPayload Payload
        {
            get { return this.payload; }
        }

        public byte[] ToByteArray()
        {
            // format is
            // 0 - command byte
            // tokenLength - Big endian
            // deviceToken - in binary
            // payloadLength - Big endian
            // payload - string
            var memoryStream = new MemoryStream();
            var tokenBytes = this.deviceToken.ToByteArray();
            var payloadJson = this.payload.ToJson();

            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write(COMMAND_BYTE);
                writer.Write((ushort) IPAddress.HostToNetworkOrder((short) tokenBytes.Length));
                writer.Write(tokenBytes);
                writer.Write((ushort) IPAddress.HostToNetworkOrder((short) payloadJson.Length));
                writer.Write(payloadJson);
            }

            return memoryStream.ToArray();
        }
    }
}