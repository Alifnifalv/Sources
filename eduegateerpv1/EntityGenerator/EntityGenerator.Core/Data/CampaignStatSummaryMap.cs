using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CampaignStatSummaryMaps", Schema = "marketing")]
    public partial class CampaignStatSummaryMap
    {
        [Key]
        public long CampaignStatSummaryMapIID { get; set; }
        public int? CampaignStatTypeID { get; set; }
        public long? CampaignID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Value { get; set; }

        [ForeignKey("CampaignID")]
        [InverseProperty("CampaignStatSummaryMaps")]
        public virtual SocailMediaCampaign Campaign { get; set; }
        [ForeignKey("CampaignStatTypeID")]
        [InverseProperty("CampaignStatSummaryMaps")]
        public virtual CampaignStatType CampaignStatType { get; set; }
    }
}
