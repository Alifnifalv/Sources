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
    public class FeeStructureClassMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeStructureClassMapDTO()
        {
            Class = new List<KeyValueDTO>();
            FeeStructure = new List<KeyValueDTO>();
            Academic = new KeyValueDTO();
            FeeStructureClassMapList = new List<FeeStructureClassMapListDTO>();
        }

        [DataMember]
        public long ClassFeeStructureMapIID { get; set; }
        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public long? FeeStructureID { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }
        [DataMember]
        public List<KeyValueDTO> Class { get; set; }
        [DataMember]
        public List<KeyValueDTO> FeeStructure { get; set; }

        [DataMember]
        public KeyValueDTO Academic { get; set; }

        [DataMember]
        public List<FeeStructureClassMapListDTO> FeeStructureClassMapList { get; set; }
    }

    [DataContract]
    public class FeeStructureClassMapListDTO
    { 

        [DataMember]
        public long ClassFeeStructureMapIID { get; set; }
        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public long? FeeStructureID { get; set; }
        [DataMember]
        public long? AcademicID { get; set; }

    }
}
