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
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Payload : IPayload
    {
        private readonly PayloadAlertMessage alert;
        private readonly int badge;
        private readonly IDictionary<string, object[]> customProperties;
        private readonly string sound;

        public Payload(PayloadAlertMessage alert, int badge, string sound, IDictionary<string, object[]> customProperties)
        {
            this.alert = alert;
            this.badge = badge;
            this.sound = sound;
            this.customProperties = customProperties;
        }

        public Payload(PayloadAlertMessage alert, int badge, string sound)
            : this(alert, badge, sound, null)
        {
        }

        public PayloadAlertMessage Alert
        {
            get { return this.alert; }
        }

        public int Badge
        {
            get { return this.badge; }
        }

        public IDictionary<string, object[]> CustomProperties
        {
            get { return this.customProperties; }
        }

        public string Sound
        {
            get { return this.sound; }
        }

        /// <summary>
        /// Formats the payload into JSON
        /// </summary>
        /// <returns>
        /// The JSON formatted payload
        /// </returns>
        public string ToJson()
        {
            var json = new JObject();
            var aps = new JObject();

            if (!string.IsNullOrEmpty(this.Alert.Body)
                && string.IsNullOrEmpty(this.Alert.LocalizedKey)
                && string.IsNullOrEmpty(this.Alert.ActionLocalizedKey)
                && (this.Alert.LocalizedArguments == null || this.Alert.LocalizedArguments.Length <= 0))
            {
                aps["alert"] = new JValue(this.Alert.Body);
            }
            else
            {
                JObject jsonAlert = new JObject();

                if (!string.IsNullOrEmpty(this.Alert.LocalizedKey))
                {
                    jsonAlert["loc-key"] = new JValue(this.Alert.LocalizedKey);
                }

                if (this.Alert.LocalizedArguments != null && this.Alert.LocalizedArguments.Length > 0)
                {
                    jsonAlert["loc-args"] = new JArray(this.Alert.LocalizedArguments);
                }

                if (!string.IsNullOrEmpty(this.Alert.Body))
                {
                    jsonAlert["body"] = new JValue(this.Alert.Body);
                }

                if (!string.IsNullOrEmpty(this.Alert.ActionLocalizedKey))
                {
                    jsonAlert["action-loc-key"] = new JValue(this.Alert.ActionLocalizedKey);
                }

                aps["alert"] = jsonAlert;
            }

            aps["badge"] = new JValue(this.Badge);
            aps["sound"] = new JValue(this.Sound);
            json["aps"] = aps;

            if (this.customProperties != null)
            {
                foreach (var key in this.customProperties.Keys)
                {
                    if (this.customProperties[key].Length == 1)
                    {
                        json[key] = new JValue(this.customProperties[key][0]);
                    }
                    else if (this.customProperties[key].Length > 1)
                    {
                        json[key] = new JArray(this.customProperties[key]);
                    }
                }
            }

            var rawString = json.ToString(Formatting.None, null);

            var encodedString = new StringBuilder();
            foreach (var c in rawString)
            {
                if (c < 32 || c > 127)
                {
                    encodedString.Append("\\u" + string.Format("{0:x4}", Convert.ToUInt32(c)));
                }
                else
                {
                    encodedString.Append(c);
                }
            }

            return rawString;
        }
    }
}