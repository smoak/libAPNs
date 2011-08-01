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
namespace libAPNs.Test.Unit
{
    using System;
    using Notifications;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class SimpleNotificationTest
    {
        /// <summary>
        /// The to byte array_should_return_correct_bytes.
        /// </summary>
        [Test]
        public void ToByteArray_should_return_correct_bytes()
        {
            // Arrange
            var token = new DeviceToken(Guid.NewGuid().ToString().Replace("-", string.Empty) + Guid.NewGuid().ToString().Replace("-", string.Empty));
            var payload = new Payload(new PayloadAlertMessage("test"), 1, "sound.wav");
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