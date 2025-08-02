using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BranchTransferSearchView
    {
        public long HeadIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        public int? companyID { get; set; }
        [Required]
        public string PartNumber { get; set; }
        public string ShoppingCartIID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string FromBranchName { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string ToBranchName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveryDate { get; set; }
        [StringLength(500)]
        public string description { get; set; }
        [Required]
        public string Status { get; set; }
        public string BrandName { get; set; }
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public int CommentCounts { get; set; }
    }
}
