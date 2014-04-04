using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RseProvisioningApiTests.Models
{
    public class Address
    {
        private AddressType addressType;

        [Required]
        [JsonProperty("type")]
        public string Type
        {
            get
            {
                switch (addressType)
                {
                    case AddressType.RackspaceEmail:
                        return "rackspace-email";
                    case AddressType.RackspaceEmailAlias:
                        return "rackspace-email-alias";
                    case AddressType.GroupList:
                        return "grouplist";
                    case AddressType.Exchange:
                        return "exchange";
                    case AddressType.ExchangeContact:
                        return "exchange-contact";
                    case AddressType.ExchangeDistList:
                        return "exchange-distlist";
                    case AddressType.ExchangePublicFolder:
                        return "exchange-publicfolder";
                    default:
                        return addressType.ToString();
                }
            }
            set
            {
                switch (value)
                {
                    case "rackspace-email":
                        addressType = AddressType.RackspaceEmail;
                        break;
                    case "rackspace-email-alias":
                        addressType = AddressType.RackspaceEmailAlias;
                        break;
                    case "grouplist":
                        addressType = AddressType.GroupList;
                        break;
                    case "exchange":
                        addressType = AddressType.Exchange;
                        break;
                    case "exchange-contact":
                        addressType = AddressType.ExchangeContact;
                        break;
                    case "exchange-distlist":
                        addressType = AddressType.ExchangeDistList;
                        break;
                    case "exchange-publicfolder":
                        addressType = AddressType.ExchangePublicFolder;
                        break;
                    default:
                        addressType = (AddressType)Enum.Parse(typeof(AddressType), value);
                        break;
                }   
            }
        }

        [JsonProperty("href")]
        public string HRef { get; set; }
    }
}
