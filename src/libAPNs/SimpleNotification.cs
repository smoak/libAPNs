// -----------------------------------------------------------------------
// <copyright file="Notification.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs
{
    using System;
    using System.IO;
    using System.Net;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SimpleNotification : ISimpleNotification
    {
        private IPayload payload;

        private DeviceToken deviceToken;

        internal const byte COMMAND_BYTE = 0;

        public SimpleNotification(DeviceToken token, IPayload payload)
        {
            this.deviceToken = token;
            this.payload = payload;
        }

        public IPayload Payload
        {
            get { return this.payload; }
        }

        public DeviceToken DeviceToken
        {
            get { return this.deviceToken; }
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
                writer.Write((ushort)IPAddress.HostToNetworkOrder((short)tokenBytes.Length));
                writer.Write(tokenBytes);
                writer.Write((ushort)IPAddress.HostToNetworkOrder((short)payloadJson.Length));
                writer.Write(payloadJson);
            }

            return memoryStream.ToArray();
        }
    }
}
