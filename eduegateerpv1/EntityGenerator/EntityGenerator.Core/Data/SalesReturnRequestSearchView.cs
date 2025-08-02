using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalesReturnRequestSearchView
    {
        public long HeadIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(767)]
        public string Customer { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveryDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public string ShoppingCartIID { get; set; }
        public int? CompanyID { get; set; }
        [Required]
        public string PartNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SINumber { get; set; }
        [Required]
        public string PickedFrom { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SONumber { get; set; }
        public int CommentCounts { get; set; }
    }
}
