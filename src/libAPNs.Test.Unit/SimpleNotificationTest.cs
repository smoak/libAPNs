// -----------------------------------------------------------------------
// <copyright file="SimpleNotificationTest.cs" company="Microsoft">
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
    public class SimpleNotificationTest
    {
        [Test]
        public void ToByteArray_should_return_correct_bytes()
        {
            // Arrange
            var token = new DeviceToken(Guid.NewGuid().ToString().Replace("-", string.Empty) + Guid.NewGuid().ToString().Replace("-", string.Empty));
            var payload = new Payload("test", 1, "sound.wav");
            var simpleNotification = new SimpleNotification(token, payload);

            // Act
            var result = simpleNotification.ToByteArray();

            // Assert
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(0, result[1]);
            Assert.AreEqual(0x20, result[2]);
            Assert.AreEqual(0x00, result[35]);
            Assert.AreEqual(payload.ToJson().Length, result[36]);
        }
    }
}
