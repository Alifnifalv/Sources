using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BidToPurchaseOrderView
    {
        public long HeadIID { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public byte? SchoolID { get; set; }
        public long? ReferenceHeadID { get; set; }
        public long? SupplierID { get; set; }
        [StringLength(50)]
        public string Supplier { get; set; }
        public int? DocumentTypeID { get; set; }
        public long? DocumentStatusID { get; set; }
        public byte? TransactionStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(250)]
        public string Bid { get; set; }
        [StringLength(50)]
        public string TransactionStatus { get; set; }
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [StringLength(50)]
        public string Telephone { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string QT { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
