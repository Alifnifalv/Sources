using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Promotions", Schema = "cms")]
    public partial class Promotion
    {
        public Promotion()
        {
            PromotionLogs = new HashSet<PromotionLog>();
        }

        [Key]
        public long PromotionIID { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public byte? PromotionTypeID { get; set; }
        [StringLength(500)]
        public string PromotionFile { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? TotalCount { get; set; }
        public int? RemainingCount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Points { get; set; }
        public int? SortOrder { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? DiscountAmount { get; set; }
        public int? DiscountPercentage { get; set; }
        public long? ReferenceID { get; set; }

        [ForeignKey("PromotionTypeID")]
        [InverseProperty("Promotions")]
        public virtual PromotionType PromotionType { get; set; }
        [InverseProperty("Promotion")]
        public virtual ICollection<PromotionLog> PromotionLogs { get; set; }
    }
}
