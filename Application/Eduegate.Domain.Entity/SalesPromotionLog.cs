namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.SalesPromotionLogs")]
    public partial class SalesPromotionLog
    {
        [Key]
        public long SalesPromotionLogIID { get; set; }

        public long? SalesPromotionID { get; set; }

        public long? LoginID { get; set; }

        public long? CustomerID { get; set; }

        public long? EmployeeID { get; set; }

        public DateTime? TransactionDate { get; set; }

        public decimal? DiscountAmountGiven { get; set; }

        public double? DiscountPercentageGiven { get; set; }

        public decimal? PointsGiven { get; set; }

        public int? CurrentTotalCount { get; set; }

        public int? CurrentRemainingCount { get; set; }

        public long? ReferenceID { get; set; }

        public virtual Login Login { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual SalesPromotion SalesPromotion { get; set; }
    }
}
