// -----------------------------------------------------------------------
// <copyright file="DeviceTokenTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace libAPNs.Test.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class DeviceTokenTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_should_validate_token()
        {
            var token = new DeviceToken("testing-with weird. symbols;");
        }
    }
}
