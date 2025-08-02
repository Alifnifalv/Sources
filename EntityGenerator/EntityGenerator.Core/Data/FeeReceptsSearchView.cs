using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeReceptsSearchView
    {
        public long FeeCollectionIID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string Grade { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        public long? StudentID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        public long? StudentIID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string CollectionDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalCollectAmount { get; set; }
        public bool? TotalPaid { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(20)]
        public string AcademicYear { get; set; }
        public int? PaymentModeID { get; set; }
        [StringLength(50)]
        public string PaymentModeName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PayAmount { get; set; }
        [Required]
        [StringLength(50)]
        public string FeePeriod { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Required]
        [StringLength(302)]
        public string ParentName { get; set; }
    }
}
