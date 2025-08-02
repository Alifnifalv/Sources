using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SupplierTransactionSearchView
    {
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public long HeadIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string InventoryTypeName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public long? CustomerID { get; set; }
        [Required]
        [StringLength(765)]
        public string CustomerName { get; set; }
        public long? SupplierID { get; set; }
        [Required]
        [StringLength(765)]
        public string SupplierName { get; set; }
        [StringLength(50)]
        public string TransactionStatus { get; set; }
        [StringLength(50)]
        public string MobileNo1 { get; set; }
        [StringLength(100)]
        public string LoginEmailID { get; set; }
        public long? BranchID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        public string ShoppingCartIID { get; set; }
        public string comments { get; set; }
        [StringLength(50)]
        public string JobStatus { get; set; }
    }
}
