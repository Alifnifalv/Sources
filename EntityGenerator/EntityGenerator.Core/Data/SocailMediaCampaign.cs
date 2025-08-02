using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SocailMediaCampaigns", Schema = "marketing")]
    public partial class SocailMediaCampaign
    {
        public SocailMediaCampaign()
        {
            CampaignEmployeeMaps = new HashSet<CampaignEmployeeMap>();
            CampaignStatSummaryMaps = new HashSet<CampaignStatSummaryMap>();
            SocailMediaCampaignTags = new HashSet<SocailMediaCampaignTag>();
        }

        [Key]
        public long SocailMediaCampaignIID { get; set; }
        public long? CampaignID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CampaignID")]
        [InverseProperty("SocailMediaCampaigns")]
        public virtual Campaign Campaign { get; set; }
        [InverseProperty("Compaign")]
        public virtual ICollection<CampaignEmployeeMap> CampaignEmployeeMaps { get; set; }
        [InverseProperty("Campaign")]
        public virtual ICollection<CampaignStatSummaryMap> CampaignStatSummaryMaps { get; set; }
        [InverseProperty("SocailMediaCampaign")]
        public virtual ICollection<SocailMediaCampaignTag> SocailMediaCampaignTags { get; set; }
    }
}
