using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeDueCancellations", Schema = "schools")]
    public partial class FeeDueCancellation
    {
        [Key]
        public long FeeDueCancellationIID { get; set; }
        public long? StudentID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsCancelled { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelationDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("FeeDueCancellations")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("FeeDueFeeTypeMapsID")]
        [InverseProperty("FeeDueCancellations")]
        public virtual FeeDueFeeTypeMap FeeDueFeeTypeMaps { get; set; }
        [ForeignKey("StudentFeeDueID")]
        [InverseProperty("FeeDueCancellations")]
        public virtual StudentFeeDue StudentFeeDue { get; set; }
    }
}
