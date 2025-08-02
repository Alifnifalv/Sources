namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeDueCancellations", Schema = "schools")]
    public partial class FeeDueCancellation
    {
        [Key]
        public long FeeDueCancellationIID { get; set; }

        public DateTime? CancelationDate { get; set; }

        public long? StudentID { get; set; }

        public int? AcademicYearID { get; set; }

        public bool? IsCancelled { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        public long? StudentFeeDueID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual StudentFeeDue StudentFeeDue { get; set; }

        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMap { get; set; }
    }
}
