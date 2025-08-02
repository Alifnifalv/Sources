using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Schedulers
{
    [DataContract]
    public class GeoLocationLogDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long GeoLocationLogIID { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }
        [DataMember]
        public string ReferenceID1 { get; set; }
        [DataMember]
        public string ReferenceID2 { get; set; }
        [DataMember]
        public string ReferenceID3 { get; set; }
    }
}
