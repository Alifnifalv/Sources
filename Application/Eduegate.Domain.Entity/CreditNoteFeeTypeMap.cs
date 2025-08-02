namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.CreditNoteFeeTypeMaps")]
    public partial class CreditNoteFeeTypeMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CreditNoteFeeTypeMap()
        {
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
        }

        [Key]
        public long CreditNoteFeeTypeMapIID { get; set; }

        public long? SchoolCreditNoteID { get; set; }

        public decimal? Amount { get; set; }

        public int? FeeMasterID { get; set; }

        public bool Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? PeriodID { get; set; }

        public long? AccountTransactionHeadID { get; set; }

        public int? Year { get; set; }

        public int? MonthID { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        public long? FeeDueMonthlySplitID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMap { get; set; }

        public virtual FeeDueMonthlySplit FeeDueMonthlySplit { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }

        public virtual FeePeriod FeePeriod { get; set; }

        public virtual School School { get; set; }

        public virtual SchoolCreditNote SchoolCreditNote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
    }
}
