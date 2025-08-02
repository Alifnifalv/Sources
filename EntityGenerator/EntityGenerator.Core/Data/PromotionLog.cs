using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PromotionLogs", Schema = "cms")]
    public partial class PromotionLog
    {
        [Key]
        public long PromotionLogIID { get; set; }
        public long? PromotionID { get; set; }
        public long? LoginID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PointsGiven { get; set; }
        public int? CurrentTotalCount { get; set; }
        public int? CurrentRemainingCount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? ReferenceID { get; set; }

        [ForeignKey("LoginID")]
        [InverseProperty("PromotionLogs")]
        public virtual Login Login { get; set; }
        [ForeignKey("PromotionID")]
        [InverseProperty("PromotionLogs")]
        public virtual Promotion Promotion { get; set; }
    }
}
