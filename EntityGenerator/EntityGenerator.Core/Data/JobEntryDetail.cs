using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobEntryDetails", Schema = "jobs")]
    public partial class JobEntryDetail
    {
        [Key]
        public long JobEntryDetailIID { get; set; }
        public long? JobEntryHeadID { get; set; }
        public long? ProductSKUID { get; set; }
        public long? ParentJobEntryHeadID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? UnitPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        public int? LocationID { get; set; }
        public bool? IsQuantiyVerified { get; set; }
        public bool? IsBarCodeVerified { get; set; }
        public bool? IsLocationVerified { get; set; }
        public int? JobStatusID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ValidatedQuantity { get; set; }
        public int? ValidatedLocationID { get; set; }
        [StringLength(50)]
        public string ValidatedPartNo { get; set; }
        [StringLength(50)]
        public string ValidationBarCode { get; set; }
        [StringLength(100)]
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string AWBNo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ActualQuantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ActualAmount { get; set; }

        [ForeignKey("JobEntryHeadID")]
        [InverseProperty("JobEntryDetailJobEntryHeads")]
        public virtual JobEntryHead JobEntryHead { get; set; }
        [ForeignKey("JobStatusID")]
        [InverseProperty("JobEntryDetails")]
        public virtual JobStatus JobStatus { get; set; }
        [ForeignKey("ParentJobEntryHeadID")]
        [InverseProperty("JobEntryDetailParentJobEntryHeads")]
        public virtual JobEntryHead ParentJobEntryHead { get; set; }
        [ForeignKey("ProductSKUID")]
        [InverseProperty("JobEntryDetails")]
        public virtual ProductSKUMap ProductSKU { get; set; }
    }
}
