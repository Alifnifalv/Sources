using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DUP_NON_REGULAR_FEE_20220412
    {
        [StringLength(50)]
        public string FeeName { get; set; }
        public long FeeCollectionID { get; set; }
        public long? StudentID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcadamicYearID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? FeeMasterID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? FeeDue { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
    }
}
