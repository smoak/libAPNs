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
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class DeviceTokenTest
    {
        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void Constructor_should_validate_token()
        {
            var token = new DeviceToken("testing-with weird. symbols;");
        }

        [Test]
        public void ToBytes_should_return_correct_bytes()
        {
            // Arrange
            var token = new DeviceToken("fe58fc8f527c363d1b775dca133e04bff24dc5032d08836992395cc56bfa62ef");
            var realTokenBytes = new byte[] { 0xfe, 0x58, 0xfc, 0x8f, 0x52, 0x7c, 0x36, 0x3d, 
                0x1b,0x77,0x5d,0xca,0x13,0x3e,0x04,0xbf,0xf2,0x4d,0xc5,0x03,0x2d,0x08,0x83,0x69,0x92,0x39,0x5c,0xc5,0x6b,0xfa,0x62,0xef 
            };

            // Act
            var tokenBytes = token.ToByteArray();

            // Assert
            Assert.AreEqual(32, tokenBytes.Length);
            Assert.AreEqual(tokenBytes, realTokenBytes);
        }

        [Test]
        public void FromBinary_should_return_correct_token()
        {
            // Arrange
            var tokenBytes = new byte[] { 0xfe, 0x58, 0xfc, 0x8f, 0x52, 0x7c, 0x36, 0x3d, 
                0x1b,0x77,0x5d,0xca,0x13,0x3e,0x04,0xbf,0xf2,0x4d,0xc5,0x03,0x2d,0x08,0x83,0x69,0x92,0x39,0x5c,0xc5,0x6b,0xfa,0x62,0xef 
            };

            // Act
            var token = DeviceToken.FromBinary(tokenBytes);

            // Assert
            Assert.AreEqual("fe58fc8f527c363d1b775dca133e04bff24dc5032d08836992395cc56bfa62ef", token.ToString());
        }
    }
}