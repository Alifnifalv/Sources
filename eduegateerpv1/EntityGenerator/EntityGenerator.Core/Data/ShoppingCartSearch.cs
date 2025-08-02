using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ShoppingCartSearch
    {
        public long ShoppingCartID { get; set; }
        [StringLength(767)]
        public string Customer { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomerID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal DeliveryCharge { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? ProductDiscountPrice { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? ProductPrice { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? CartAmount { get; set; }
        [Required]
        public string Branches { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        [Required]
        public string Transactions { get; set; }
        public int? CompanyID { get; set; }
    }
}
