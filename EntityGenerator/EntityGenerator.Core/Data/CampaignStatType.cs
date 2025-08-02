using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CampaignStatTypes", Schema = "marketing")]
    public partial class CampaignStatType
    {
        public CampaignStatType()
        {
            CampaignStatSummaryMaps = new HashSet<CampaignStatSummaryMap>();
        }

        [Key]
        public int CampaignStatTypeID { get; set; }
        [StringLength(50)]
        public string StatName { get; set; }

        [InverseProperty("CampaignStatType")]
        public virtual ICollection<CampaignStatSummaryMap> CampaignStatSummaryMaps { get; set; }
    }
}
