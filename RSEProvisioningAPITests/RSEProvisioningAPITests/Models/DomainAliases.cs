using Newtonsoft.Json;

namespace RseProvisioningApiTests.Models
{
    public class DomainAliases
    {
        [JsonProperty("aliases")]
        public string[] Aliases { get; set; }
    }
}