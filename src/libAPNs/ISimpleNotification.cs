// -----------------------------------------------------------------------
// <copyright file="INotification.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs
{
    /// <summary>
    /// Represents a notification to send to a specific device
    /// </summary>
    public interface ISimpleNotification
    {
        IPayload Payload { get; }

        DeviceToken DeviceToken { get; }

        byte[] ToByteArray();
    }
}
