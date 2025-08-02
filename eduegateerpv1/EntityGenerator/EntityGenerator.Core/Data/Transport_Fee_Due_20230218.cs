using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Transport_Fee_Due_20230218
    {
        public bool? IsActive { get; set; }
        public bool IsCancelled { get; set; }
        public bool CollectionStatus { get; set; }
        public long StudentFeeDueIID { get; set; }
        public long? StudentId { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public int? FeeMasterID { get; set; }
        public long FeeDueFeeTypeMapsIID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        public bool? IsAccounted { get; set; }
        [Column(TypeName = "money")]
        public decimal? AccountAmount { get; set; }
        public bool? IsAccountDeleted { get; set; }
        [Column(TypeName = "money")]
        public decimal? Col_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Crn_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fnl_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Rnd_Amount { get; set; }
    }
}
