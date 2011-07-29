// -----------------------------------------------------------------------
// <copyright file="IAPNSConnections.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs.Connection
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal interface IAPNSConnection
    {

        void Connect();

        void Disconnect();

        void Write(byte[] data);

        void Write(byte[] data, int offset, int count);
        
    }
}
