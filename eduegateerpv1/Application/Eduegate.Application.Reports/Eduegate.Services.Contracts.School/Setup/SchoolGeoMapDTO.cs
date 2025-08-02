using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Setup
{
    [DataContract]
    public class SchoolGeoMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long SchoolGeoMapIID { get; set; }
        [DataMember]
        public int? SchoolID { get; set; }

        [DataMember]
        public string Longitude { get; set; }

        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public long? AreaID { get; set; }
    }
}
