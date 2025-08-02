using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Eduegate.Services.Contracts.School.Setup
{
    [DataContract]
  public  class GeoLocationLogDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
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
