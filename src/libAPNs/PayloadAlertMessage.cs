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
namespace libAPNs
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PayloadAlertMessage
    {
        public PayloadAlertMessage(string body, string actionLocalizedKey, string localizedKey, string[] localizedArguments, string launchImage)
        {
            this.Body = body;
            this.ActionLocalizedKey = actionLocalizedKey;
            this.LocalizedArguments = localizedArguments;
            this.LocalizedKey = localizedKey;
            this.LaunchImage = launchImage;
        }

        public PayloadAlertMessage(string body)
            : this(body, null, null, null, null)
        {
        }

        public PayloadAlertMessage(string body, string actionLocalizedKey)
            : this(body, actionLocalizedKey, null, null, null)
        {
        }

        public PayloadAlertMessage(string body, string actionLocalizedKey, string localizedKey)
            : this(body, actionLocalizedKey, localizedKey, null, null)
        {
        }

        public PayloadAlertMessage(string body, string actionLocalizedKey, string localizedKey, string[] localizedArguments)
            : this(body, actionLocalizedKey, localizedKey, localizedArguments, null)
        {
        }

        public string ActionLocalizedKey { get; private set; }
        public string Body { get; private set; }
        public string LaunchImage { get; private set; }
        public string[] LocalizedArguments { get; private set; }
        public string LocalizedKey { get; private set; }
    }
}