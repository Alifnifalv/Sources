using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeePaymentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeePaymentDTO()
        {
            StudentFeeDueTypes = new List<FeeDueFeeTypeMapDTO>();
        }

        [DataMember]
        public List<FeeCollectionDTO> FeeCollection { get; set; }

        [DataMember]
        public List<StudentFeeDueDTO> StudentFeeDues { get; set; }

        [DataMember]
        public List<FeeDueFeeTypeMapDTO> StudentFeeDueTypes { get; set; }

        [DataMember]
        public bool IsExpand { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public decimal? TotalAmount { get; set; }

        [DataMember]
        public decimal? OldTotalAmount { get; set; }

        [DataMember]
        public decimal? NowPaying { get; set; }

        [DataMember]
        public bool? IsSelected { get; set; }
    }
}