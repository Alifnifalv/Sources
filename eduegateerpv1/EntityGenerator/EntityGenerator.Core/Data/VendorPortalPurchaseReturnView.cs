using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VendorPortalPurchaseReturnView
    {
        public long HeadIID { get; set; }
        public long LoginID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public byte? SchoolID { get; set; }
        public long? SupplierID { get; set; }
        [StringLength(50)]
        public string Supplier { get; set; }
        public int? DocumentTypeID { get; set; }
        public long? DocumentStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(50)]
        public string TransactionStatus { get; set; }
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [StringLength(50)]
        public string Telephone { get; set; }
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
