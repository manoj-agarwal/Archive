using Newtonsoft.Json;

namespace RseProvisioningApiTests.Models
{
    public class EmailAlias
    {
        [JsonProperty("email")]
        public string Alias { get; set; }

        [JsonProperty("addresses")]
        public string[] Addresses { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var a = obj as EmailAlias;
            if (a == null)
            {
                return false;
            }

            return (Alias == a.Alias &&
                    Helper.ArraysEqual(Addresses, a.Addresses));
        }
    }
}