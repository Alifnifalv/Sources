using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SocailMediaCampaignTags", Schema = "marketing")]
    public partial class SocailMediaCampaignTag
    {
        public SocailMediaCampaignTag()
        {
            InverseCampaignTag = new HashSet<SocailMediaCampaignTag>();
        }

        [Key]
        public long SocailMediaCampaignTagIID { get; set; }
        public long? SocailMediaCampaignID { get; set; }
        public long? CampaignTagID { get; set; }

        [ForeignKey("CampaignTagID")]
        [InverseProperty("InverseCampaignTag")]
        public virtual SocailMediaCampaignTag CampaignTag { get; set; }
        [ForeignKey("SocailMediaCampaignID")]
        [InverseProperty("SocailMediaCampaignTags")]
        public virtual SocailMediaCampaign SocailMediaCampaign { get; set; }
        [InverseProperty("CampaignTag")]
        public virtual ICollection<SocailMediaCampaignTag> InverseCampaignTag { get; set; }
    }
}
