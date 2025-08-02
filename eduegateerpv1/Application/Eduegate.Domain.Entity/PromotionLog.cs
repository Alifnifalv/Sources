namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.PromotionLogs")]
    public partial class PromotionLog
    {
        [Key]
        public long PromotionLogIID { get; set; }

        public long? PromotionID { get; set; }

        public long? LoginID { get; set; }

        public decimal? PointsGiven { get; set; }

        public int? CurrentTotalCount { get; set; }

        public int? CurrentRemainingCount { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? ReferenceID { get; set; }

        public virtual Login Login { get; set; }

        public virtual Promotion Promotion { get; set; }
    }
}
