// -----------------------------------------------------------------------
// <copyright file="IPayload.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IPayload
    {
        string Alert { get; }

        int Badge { get; }

        string Custom { get; }

        string Sound { get; }

        string ToJson();

    }
}
