using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Jobs
{
    [Table("JobEntryDetails", Schema = "jobs")]
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

        public byte[] TimeStamps { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string AWBNo { get; set; }

        public decimal? Amount { get; set; }

        public decimal? ActualQuantity { get; set; }

        public decimal? ActualAmount { get; set; }

        public virtual JobEntryHead JobEntryHead { get; set; }

        public virtual JobStatus JobStatus { get; set; }

        public virtual JobEntryHead ParentJobEntryHead { get; set; }
    }
}