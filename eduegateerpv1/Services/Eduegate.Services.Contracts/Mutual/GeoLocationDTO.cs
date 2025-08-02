using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
   public class GeoLocationDTO
    {
        [DataMember]
        public string ip { get; set; }
        [DataMember]
        public string country_code { get; set; }
        [DataMember]
        public string country_name { get; set; }
        [DataMember]
        public string region_code { get; set; }
        [DataMember]
        public string region_name { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string zip_code { get; set; }
        [DataMember]
        public string time_zone { get; set; }
        [DataMember]
        public float latitude { get; set; }
        [DataMember]
        public float longitude { get; set; }
        [DataMember]
        public int metro_code { get; set; }
    }
}
