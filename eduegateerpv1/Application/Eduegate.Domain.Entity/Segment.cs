namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.Segments")]
    public partial class Segment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Segment()
        {
            EmailCampaigns = new HashSet<EmailCampaign>();
            SegmentCustomerMaps = new HashSet<SegmentCustomerMap>();
        }

        [Key]
        public long SegmentIID { get; set; }

        [StringLength(50)]
        public string SegmentName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailCampaign> EmailCampaigns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SegmentCustomerMap> SegmentCustomerMaps { get; set; }
    }
}
