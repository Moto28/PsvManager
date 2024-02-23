using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PsvManager.Shared.DTO.Address
{
    [DataContract]
    public class AddressDto
    {
        [DataMember]
        public string HouseNumber { get; set; }
        [DataMember]
        public string StreetName { get; set; }
        [DataMember]
        public string TownOrCity { get; set; }
        [DataMember]
        public string? County { get; set; }
        [DataMember]
        public string Postcode { get; set; }
    }
}
