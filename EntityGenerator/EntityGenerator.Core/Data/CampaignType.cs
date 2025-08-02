using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CampaignTypes", Schema = "marketing")]
    public partial class CampaignType
    {
        public CampaignType()
        {
            Campaigns = new HashSet<Campaign>();
        }

        [Key]
        public int CampaignTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("CampaignType")]
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
