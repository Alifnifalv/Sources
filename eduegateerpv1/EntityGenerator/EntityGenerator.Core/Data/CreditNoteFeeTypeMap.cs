using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CreditNoteFeeTypeMaps", Schema = "schools")]
    [Index("FeeDueMonthlySplitID", Name = "IDX_CreditNoteFeeTypeMaps_FeeDueMonthlySplitID_")]
    [Index("FeeMasterID", Name = "IDX_CreditNoteFeeTypeMaps_FeeMasterID_SchoolCreditNoteID__Amount__FeeDueFeeTypeMapsID")]
    [Index("FeeDueFeeTypeMapsID", Name = "idx_CreditNoteFeeTypeMapsFeeDueFeeTypeMapsID")]
    [Index("SchoolCreditNoteID", Name = "idx_CreditNoteFeeTypeMapsSchoolCreditNoteID")]
    public partial class CreditNoteFeeTypeMap
    {
        public CreditNoteFeeTypeMap()
        {
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
        }

        [Key]
        public long CreditNoteFeeTypeMapIID { get; set; }
        public long? SchoolCreditNoteID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int? FeeMasterID { get; set; }
        public bool Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? PeriodID { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        public int? Year { get; set; }
        public int? MonthID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? FeeDueMonthlySplitID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("CreditNoteFeeTypeMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("CreditNoteFeeTypeMaps")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("FeeDueMonthlySplitID")]
        [InverseProperty("CreditNoteFeeTypeMaps")]
        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("CreditNoteFeeTypeMaps")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("PeriodID")]
        [InverseProperty("CreditNoteFeeTypeMaps")]
        public virtual FeePeriod Period { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("CreditNoteFeeTypeMaps")]
        public virtual School School { get; set; }
        [ForeignKey("SchoolCreditNoteID")]
        [InverseProperty("CreditNoteFeeTypeMaps")]
        public virtual SchoolCreditNote SchoolCreditNote { get; set; }
        [InverseProperty("CreditNoteFeeTypeMap")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
    }
}
