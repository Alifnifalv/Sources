using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentFeeConcessions", Schema = "schools")]
    public partial class StudentFeeConcession
    {
        [Key]
        public long StudentFeeConcessionIID { get; set; }
        public long StudentGroupFeeMasterID { get; set; }
        public long? StudentID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        public bool? IsPercentage { get; set; }
        public short? ConcessionApprovalTypeID { get; set; }
        public int? StudentGroupID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PercentageAmount { get; set; }
        public string Formula { get; set; }
        public byte? SchoolID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StaffID { get; set; }
        public long? ParentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NetAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DueAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConcessionAmount { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? CreditNoteID { get; set; }
        public long? CreditNoteFeeTypeMapID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConcessionDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ConcessionApprovalTypeID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual FeeConcessionApprovalType ConcessionApprovalType { get; set; }
        [ForeignKey("CreditNoteFeeTypeMapID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual CreditNoteFeeTypeMap CreditNoteFeeTypeMap { get; set; }
        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("FeeMasterID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual FeeMaster FeeMaster { get; set; }
        [ForeignKey("FeePeriodID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual FeePeriod FeePeriod { get; set; }
        [ForeignKey("ParentID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("StaffID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("StudentFeeDueID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual StudentFeeDue StudentFeeDue { get; set; }
        [ForeignKey("StudentGroupID")]
        [InverseProperty("StudentFeeConcessions")]
        public virtual StudentGroup StudentGroup { get; set; }
    }
}
