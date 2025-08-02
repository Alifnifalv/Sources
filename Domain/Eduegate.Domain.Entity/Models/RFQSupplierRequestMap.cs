using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Entity.Models.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("RFQSupplierRequestMap", Schema = "inventory")]
    public partial class RFQSupplierRequestMap
    {
        public RFQSupplierRequestMap() 
        {

        }

        [Key]
        public long RFQMapID { get; set; }
        public long? SupplierID { get; set; }
        public long? PurchaseRequestID { get; set; }
        public long? HeadID { get; set; }
        public long? TenderID { get; set; }
        public string Remarks { get; set; }
        public virtual TransactionHead Head { get; set; }
        public virtual TransactionHead PurchaseRequest { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual Tender Tender { get; set; }

    }
}