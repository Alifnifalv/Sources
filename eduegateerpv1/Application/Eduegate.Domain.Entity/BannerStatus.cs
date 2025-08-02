namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.BannerStatuses")]
    public partial class BannerStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BannerStatus()
        {
            Banners = new HashSet<Banner>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BannerStatusID { get; set; }

        [StringLength(100)]
        public string BannerStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banner> Banners { get; set; }
    }
}
