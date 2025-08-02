namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobs.JobEntryDetails")]
    public partial class JobEntryDetail
    {
        [Key]
        public long JobEntryDetailIID { get; set; }

        public long? JobEntryHeadID { get; set; }

        public long? ProductSKUID { get; set; }

        public long? ParentJobEntryHeadID { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? Quantity { get; set; }

        public int? LocationID { get; set; }

        public bool? IsQuantiyVerified { get; set; }

        public bool? IsBarCodeVerified { get; set; }

        public bool? IsLocationVerified { get; set; }

        public int? JobStatusID { get; set; }

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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(50)]
        public string AWBNo { get; set; }

        public decimal? Amount { get; set; }

        public decimal? ActualQuantity { get; set; }

        public decimal? ActualAmount { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual JobEntryHead JobEntryHead { get; set; }

        public virtual JobEntryHead JobEntryHead1 { get; set; }

        public virtual JobStatus JobStatus { get; set; }
    }
}
