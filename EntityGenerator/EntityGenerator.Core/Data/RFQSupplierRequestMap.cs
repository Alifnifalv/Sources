using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RFQSupplierRequestMap", Schema = "inventory")]
    public partial class RFQSupplierRequestMap
    {
        [Key]
        public long RFQMapID { get; set; }
        public long? SupplierID { get; set; }
        public long? PurchaseRequestID { get; set; }
        public long? HeadID { get; set; }
        public string Remarks { get; set; }
        public long? TenderID { get; set; }

        [ForeignKey("HeadID")]
        [InverseProperty("RFQSupplierRequestMapHeads")]
        public virtual TransactionHead Head { get; set; }
        [ForeignKey("PurchaseRequestID")]
        [InverseProperty("RFQSupplierRequestMapPurchaseRequests")]
        public virtual TransactionHead PurchaseRequest { get; set; }
        [ForeignKey("SupplierID")]
        [InverseProperty("RFQSupplierRequestMaps")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("TenderID")]
        [InverseProperty("RFQSupplierRequestMaps")]
        public virtual Tender Tender { get; set; }
    }
}
