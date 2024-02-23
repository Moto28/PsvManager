using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PsvManager.Shared.DTO.Driver
{
    [DataContract]
    public class DriverDto
    {
        [DataMember]
        public string Forename { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string LicenseNumber { get; set; }
    }
}
