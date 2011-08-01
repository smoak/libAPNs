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
    using System;
    using System.IO;
    using System.Net;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnhancedNotification : IEnhancedNotification
    {
        internal const byte COMMAND_BYTE = 0x01;
        private DeviceToken deviceToken;
        private IPayload payload;
        private uint identifier;
        private TimeSpan? expiry;

        private readonly DateTime unixEpoch = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);

        public EnhancedNotification(DeviceToken deviceToken, IPayload payload, uint identifier, TimeSpan? expiry)
        {
            this.deviceToken = deviceToken;
            this.payload = payload;
            this.identifier = identifier;
            this.expiry = expiry;
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
            // 1 - command byte
            // identifier - 4 bytes
            // expiry - 4 bytes big endian
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
                writer.Write(identifier);
                if (this.expiry.HasValue)
                {
                    var secondsFromEpochUtc = (int)((this.unixEpoch + this.expiry.Value) - this.unixEpoch).TotalSeconds;
                    writer.Write(IPAddress.HostToNetworkOrder(secondsFromEpochUtc));
                }
                else
                {
                    writer.Write(0);
                }
                writer.Write((ushort)IPAddress.HostToNetworkOrder((short)tokenBytes.Length));
                writer.Write(tokenBytes);
                writer.Write((ushort)IPAddress.HostToNetworkOrder((short)payloadJson.Length));
                writer.Write(payloadJson);
            }

            return memoryStream.ToArray();
        }

        /// <summary>
        /// An arbitrary value that identifies this notification. 
        /// This same identifier is returned in a error-response packet 
        /// if APNs cannot interpret a notification.
        /// </summary>
        public uint Identifier
        {
            get { return this.identifier; }
        }

        /// <summary>
        ///  Identifies when the notification is no longer valid and can be discarded. 
        ///  If set, APNs tries to deliver the notification at least once. 
        ///  If not set, APNs will not store the notification at all.
        ///  This should be based off of a fixed unix epoch date (UTC)
        /// </summary>
        public TimeSpan? Expiry
        {
            get { return this.expiry; }
        }
    }
}