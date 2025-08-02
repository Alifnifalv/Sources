namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.SalesPromotionTypes")]
    public partial class SalesPromotionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalesPromotionType()
        {
            SalesPromotions = new HashSet<SalesPromotion>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SalesPromotionTypeID { get; set; }

        [StringLength(50)]
        public string PromotionTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesPromotion> SalesPromotions { get; set; }
    }
}
