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
    public class FeeStructureDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeStructureDTO()
        {
            FeeStructureFeeMaps = new List<FeeStructureFeeMapDTO>();
           
        }

        [DataMember]
        public long FeeStructureIID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public int? AcadamicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }
        [DataMember]
        public int? CreatedBy { get; set; }
        [DataMember]
        public int? UpdatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }

        [DataMember]
        public byte[] TimeStamps { get; set; }

        [DataMember]
        public  KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public List<FeeStructureFeeMapDTO> FeeStructureFeeMaps { get; set; }
       
    }
}

