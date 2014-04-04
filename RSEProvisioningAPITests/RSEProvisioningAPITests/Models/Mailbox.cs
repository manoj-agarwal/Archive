using Newtonsoft.Json;

namespace RseProvisioningApiTests.Models
{
    public class Mailbox
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mailhost")]
        public string Mailhost { get; set; }

        [JsonProperty("forward")]
        public MailboxForwardSettings ForwardSettings { get; set; }

        [JsonProperty("vacation")]
        public string VacationMessage { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var m = obj as Mailbox;
            if (m == null)
            {
                return false;
            }

            return (Email == m.Email &&
                    Mailhost == m.Mailhost &&
                    ForwardSettings == m.ForwardSettings &&
                    VacationMessage == m.VacationMessage);
        }
    }

    public class MailboxForwardSettings
    {
        [JsonProperty("addresses")]
        public string[] Addresses { get; set; }

        [JsonProperty("save")]
        public bool Save { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var f = obj as MailboxForwardSettings;
            if (f == null)
            {
                return false;
            }

            return (Helper.ArraysEqual(Addresses, f.Addresses) &&
                    Save == f.Save);
        }
    }
}
