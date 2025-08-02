using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_FEECOLLECTION_LIVE
    {
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        public long FeeCollectionIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CollectionDate { get; set; }
        [StringLength(50)]
        public string FeeReceiptNo { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcadamicYearID { get; set; }
        [StringLength(50)]
        public string PaymentModeName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string CustomersAttend { get; set; }
    }
}
