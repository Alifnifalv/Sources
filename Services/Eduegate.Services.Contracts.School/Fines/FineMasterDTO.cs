using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fines
{
    [DataContract]
    public class FineMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int FineMasterID { get; set; }

        [DataMember]
        [StringLength(20)]
        public string FineCode { get; set; }

        [DataMember]
        [StringLength(100)]
        public string FineName { get; set; }

        [DataMember]
        public short? FeeFineTypeID { get; set; }

        [DataMember]
        public long? LedgerAccountID { get; set; }

        [DataMember]
        public string FeeFineType { get; set; }

        [DataMember]
        public string LedgerAccount { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}