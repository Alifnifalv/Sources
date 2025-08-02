namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.SalesPromotions")]
    public partial class SalesPromotion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? DiscountAmount { get; set; }

        public double? DiscountPercentage { get; set; }

        public long? ReferenceID { get; set; }

        public int? TotalCount { get; set; }

        public int? RemainingCount { get; set; }

        public long? CustomerID { get; set; }

        [StringLength(50)]
        public string PromotionKey { get; set; }

        public bool? PerCustomer { get; set; }

        public int? PerCustomerCount { get; set; }

        public bool? IsActive { get; set; }

        public double? Points { get; set; }

        public decimal? MaxDiscountAmount { get; set; }

        public decimal? MinCartAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesPromotionLog> SalesPromotionLogs { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual SalesPromotionType SalesPromotionType { get; set; }
    }
}
