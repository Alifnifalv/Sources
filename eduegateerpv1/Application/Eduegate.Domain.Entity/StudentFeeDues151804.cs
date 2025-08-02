namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentFeeDues151804")]
    public partial class StudentFeeDues151804
    {
        [Key]
        [Column(Order = 0)]
        public long StudentFeeDueIID { get; set; }

        public int? ClassId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? StudentId { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public DateTime? DueDate { get; set; }

        [StringLength(50)]
        public string InvoiceNo { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool CollectionStatus { get; set; }

        public bool? IsAccountPost { get; set; }

        public DateTime? AccountPostingDate { get; set; }

        public int? AcadamicYearID { get; set; }

        public int? SectionID { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsCancelled { get; set; }

        public DateTime? CancelledDate { get; set; }

        [StringLength(250)]
        public string CancelReason { get; set; }
    }
}
