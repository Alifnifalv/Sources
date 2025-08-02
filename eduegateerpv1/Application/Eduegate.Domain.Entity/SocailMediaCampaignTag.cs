namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.SocailMediaCampaignTags")]
    public partial class SocailMediaCampaignTag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SocailMediaCampaignTag()
        {
            SocailMediaCampaignTags1 = new HashSet<SocailMediaCampaignTag>();
        }

        [Key]
        public long SocailMediaCampaignTagIID { get; set; }

        public long? SocailMediaCampaignID { get; set; }

        public long? CampaignTagID { get; set; }

        public virtual SocailMediaCampaign SocailMediaCampaign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocailMediaCampaignTag> SocailMediaCampaignTags1 { get; set; }

        public virtual SocailMediaCampaignTag SocailMediaCampaignTag1 { get; set; }
    }
}
