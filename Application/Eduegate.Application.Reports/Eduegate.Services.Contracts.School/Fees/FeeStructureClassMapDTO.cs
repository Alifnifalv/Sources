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

        [DataMember]
        public long ClassFeeStructureMapIID { get; set; }
        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public long? FeeStructureID { get; set; }
        [DataMember]
        public int? CreatedBy { get; set; }
        [DataMember]
        public int? UpdatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        //[DataMember]
        //public byte[] TimeStamps { get; set; }
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
