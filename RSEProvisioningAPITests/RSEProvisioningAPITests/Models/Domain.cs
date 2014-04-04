using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RseProvisioningApiTests.Models
{
    public class Domain
    {
        [JsonProperty("domain")]
        public string DomainName { get; set; }

        [JsonProperty("disclaimer")]
        public Disclaimer Disclaimer { get; set; }

        [JsonProperty("externalRouting")]
        public ExternalRouting ExternalRouting { get; set; }

        [JsonProperty("catchall")]
        public string CatchAll { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var d = obj as Domain;
            if (d == null)
            {
                return false;
            }

            return (DomainName == d.DomainName &&
                    Disclaimer == d.Disclaimer &&
                    ExternalRouting == d.ExternalRouting &&
                    CatchAll == d.CatchAll);
        }
    }

    public class Disclaimer
    {
        [Required]
        [JsonProperty("plain")]
        public string Plain { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("alterSigned")]
        public bool AlterSigned { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var d = obj as Disclaimer;
            if (d == null)
            {
                return false;
            }

            return (Plain == d.Plain &&
                    Html == d.Html &&
                    AlterSigned == d.AlterSigned);
        }
    }

    public class ExternalRouting
    {
        [Required]
        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("port")]
        public string Port { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var e = obj as ExternalRouting;
            if (e == null)
            {
                return false;
            }

            return (Host == e.Host &&
                    Port == e.Port &&
                    Verified == e.Verified);
        }
    }
}
