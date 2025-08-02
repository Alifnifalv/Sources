namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.Promotions")]
    public partial class Promotion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? TotalCount { get; set; }

        public int? RemainingCount { get; set; }

        public decimal? Points { get; set; }

        public int? SortOrder { get; set; }

        public decimal? DiscountAmount { get; set; }

        public int? DiscountPercentage { get; set; }

        public long? ReferenceID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PromotionLog> PromotionLogs { get; set; }

        public virtual PromotionType PromotionType { get; set; }
    }
}
