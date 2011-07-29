namespace libAPNs
{
    using fastJSON;

    public class Payload : IPayload
    {

        private string alert, custom, sound;
        private int badge;
        

        public Payload(string alert, int badge, string sound, string custom)
        {
            this.alert = alert;
            this.badge = badge;
            this.sound = sound;
            this.custom = custom;
        }

        public Payload(string alert, int badge, string sound)
            : this(alert, badge, sound, null)
        {

        }

        public string Alert
        {
            get { return this.alert; }
        }

        public int Badge
        {
            get { return this.badge; }
        }

        public string Custom
        {
            get { return this.custom; }
        }

        public string Sound
        {
            get { return this.sound; }
        }

        public string ToJson()
        {
            return JSON.Instance.ToJSON(this);
        }
    }
}