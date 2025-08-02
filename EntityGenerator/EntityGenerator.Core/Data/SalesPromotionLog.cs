using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalesPromotionLogs", Schema = "marketing")]
    public partial class SalesPromotionLog
    {
        [Key]
        public long SalesPromotionLogIID { get; set; }
        public long? SalesPromotionID { get; set; }
        public long? LoginID { get; set; }
        public long? CustomerID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountAmountGiven { get; set; }
        public double? DiscountPercentageGiven { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PointsGiven { get; set; }
        public int? CurrentTotalCount { get; set; }
        public int? CurrentRemainingCount { get; set; }
        public long? ReferenceID { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("SalesPromotionLogs")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("SalesPromotionLogs")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("SalesPromotionLogs")]
        public virtual Login Login { get; set; }
        [ForeignKey("SalesPromotionID")]
        [InverseProperty("SalesPromotionLogs")]
        public virtual SalesPromotion SalesPromotion { get; set; }
    }
}
