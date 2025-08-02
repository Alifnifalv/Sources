using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class ClassFeeMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassFeeMasterDTO()
        {
            FeeMasterClassMaps = new List<FeeMasterClassFeePeriodDTO>();
        }

        [DataMember]
        public long ClassFeeMasterIID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<KeyValueDTO> Class { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public long? PackageConfigID { get; set; }

        [DataMember]
        public byte? FeeCycleID { get; set; }

        [DataMember]
        public KeyValueDTO Academic { get; set; }

        [DataMember]
        public KeyValueDTO Package { get; set; }

        [DataMember]
        public int? AcadamicYearID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public List<FeeMasterClassFeePeriodDTO> FeeMasterClassMaps { get; set; }
    }
}
