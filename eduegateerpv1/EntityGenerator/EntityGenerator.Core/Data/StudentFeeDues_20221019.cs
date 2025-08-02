using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("StudentFeeDues_20221019", Schema = "schools")]
    public partial class StudentFeeDues_20221019
    {
        public long StudentFeeDueIID { get; set; }
        public int? ClassId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StudentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public bool CollectionStatus { get; set; }
        public bool? IsAccountPost { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AccountPostingDate { get; set; }
        public int? AcadamicYearID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public bool? IsCancelled { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelledDate { get; set; }
        [StringLength(250)]
        public string CancelReason { get; set; }
    }
}
