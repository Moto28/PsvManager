using PsvManager.Shared.DTO.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PsvManager.Shared.DTO.Driver
{
    [DataContract]
    public class DriverWithAddressDto
    {
        [DataMember]
        public DriverDto Driver { get; set; }
        [DataMember]
        public AddressDto Address { get; set; }
    }
}
