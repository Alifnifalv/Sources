using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
   public class CostCenterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int CostCenterID { get; set; }
        [DataMember]

        public string CostCenterCode { get; set; }

        [DataMember]
        public string CostCenterName { get; set; }

        //[DataMember]
        //public int? CreatedBy { get; set; }

        //[DataMember]
        //public DateTime? CreatedDate { get; set; }

        //[DataMember]
        //public int? UpdatedBy { get; set; }

        //[DataMember]
        //public DateTime? UpdatedDate { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public bool? IsAffect_A { get; set; }

        [DataMember]
        public bool? IsAffect_L { get; set; }

        [DataMember]
        public bool? IsAffect_C { get; set; }

        [DataMember]
        public bool? IsAffect_E { get; set; }

        [DataMember]
        public bool? IsAffect_I { get; set; }

        [DataMember]
        public bool? IsFixed { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public List<CostCenterAccountMapDTO> CostCenterAccountMap { get; set; }
    }
}
