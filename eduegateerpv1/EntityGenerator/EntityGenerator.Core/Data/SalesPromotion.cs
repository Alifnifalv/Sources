using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalesPromotions", Schema = "marketing")]
    public partial class SalesPromotion
    {
        public SalesPromotion()
        {
            SalesPromotionLogs = new HashSet<SalesPromotionLog>();
        }

        [Key]
        public long SalesPromotionIID { get; set; }
        [StringLength(50)]
        public string PromotionCode { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public int? SalesPromotionTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmount { get; set; }
        public double? DiscountPercentage { get; set; }
        public long? ReferenceID { get; set; }
        public int? TotalCount { get; set; }
        public int? RemainingCount { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PromotionKey { get; set; }
        public bool? PerCustomer { get; set; }
        public int? PerCustomerCount { get; set; }
        public bool? IsActive { get; set; }
        public double? Points { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaxDiscountAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinCartAmount { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("SalesPromotions")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("SalesPromotionTypeID")]
        [InverseProperty("SalesPromotions")]
        public virtual SalesPromotionType SalesPromotionType { get; set; }
        [InverseProperty("SalesPromotion")]
        public virtual ICollection<SalesPromotionLog> SalesPromotionLogs { get; set; }
    }
}
