namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.Campaigns")]
    public partial class Campaign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Campaign()
        {
            EmailCampaigns = new HashSet<EmailCampaign>();
            SocailMediaCampaigns = new HashSet<SocailMediaCampaign>();
        }

        [Key]
        public long CampaignIID { get; set; }

        [StringLength(50)]
        public string CampaignCode { get; set; }

        public int? CampaignTypeID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

        public virtual CampaignType CampaignType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailCampaign> EmailCampaigns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocailMediaCampaign> SocailMediaCampaigns { get; set; }
    }
}
