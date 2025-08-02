namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentFeeConcessions")]
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

        public decimal? PercentageAmount { get; set; }

        public string Formula { get; set; }

        public byte? SchoolID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? StaffID { get; set; }

        public long? ParentID { get; set; }

        public decimal? NetAmount { get; set; }

        public decimal? DueAmount { get; set; }

        public decimal? ConcessionAmount { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public long? CreditNoteID { get; set; }

        public long? CreditNoteFeeTypeMapID { get; set; }

        public DateTime? ConcessionDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual CreditNoteFeeTypeMap CreditNoteFeeTypeMap { get; set; }

        public virtual FeeConcessionApprovalType FeeConcessionApprovalType { get; set; }

        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMap { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual StudentFeeDue StudentFeeDue { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
    }
}
