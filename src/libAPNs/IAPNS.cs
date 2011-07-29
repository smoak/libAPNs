// -----------------------------------------------------------------------
// <copyright file="IAPNS.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IAPNS
    {
        void SendSimpleNotification(ISimpleNotification simpleNotification);
    }
}
