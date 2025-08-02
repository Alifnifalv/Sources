using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Campaigns", Schema = "marketing")]
    public partial class Campaign
    {
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
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("CampaignTypeID")]
        [InverseProperty("Campaigns")]
        public virtual CampaignType CampaignType { get; set; }
        [InverseProperty("Campaign")]
        public virtual ICollection<EmailCampaign> EmailCampaigns { get; set; }
        [InverseProperty("Campaign")]
        public virtual ICollection<SocailMediaCampaign> SocailMediaCampaigns { get; set; }
    }
}
