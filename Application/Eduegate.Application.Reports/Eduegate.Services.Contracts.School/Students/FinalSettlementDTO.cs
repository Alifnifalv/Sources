using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public  class FinalSettlementDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FinalSettlementDTO()
        {
            FeeTypes = new List<FeeCollectionFeeTypeDTO>();
            FeeFines = new List<FeeCollectionFeeFinesDTO>();
            FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>();
        }

        [DataMember]
        public long FinalSettlementIID { get; set; }
        [DataMember]
        public long  FeeCollectionIID { get; set; }
        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public long? ClassFeeMasterId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string AdmissionNo { get; set; }

        [DataMember]
        public decimal? CollectedAmount { get; set; }

        [DataMember]
        public int? SectionID { get; set; }
        [DataMember]
        public string SectionName { get; set; }
        [DataMember]
        public long? StudentID { get; set; }
        [DataMember]
        public string StudentName { get; set; }
        [DataMember]
        public System.DateTime? SettlementDate { get; set; }

        [DataMember]
        public System.DateTime? CollectionDate { get; set; }

        [DataMember]
        public List<FeeCollectionPaymentModeMapDTO> FeeCollectionPaymentModeMapDTO { get; set; }

        [DataMember]
        public List<FeeCollectionFeeTypeDTO> FeeTypes { get; set; }

        [DataMember]
        public List<FeeCollectionFeeFinesDTO> FeeFines { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }
        [DataMember]
        public int? AcadamicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        
    }
}
