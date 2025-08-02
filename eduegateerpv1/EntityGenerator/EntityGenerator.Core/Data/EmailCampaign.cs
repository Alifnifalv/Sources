using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmailCampaigns", Schema = "marketing")]
    public partial class EmailCampaign
    {
        [Key]
        public long EmailCampaignIID { get; set; }
        public long? CampaignID { get; set; }
        public long? SegmentID { get; set; }
        public long? EmailTemplateID { get; set; }

        [ForeignKey("CampaignID")]
        [InverseProperty("EmailCampaigns")]
        public virtual Campaign Campaign { get; set; }
        [ForeignKey("EmailTemplateID")]
        [InverseProperty("EmailCampaigns")]
        public virtual EmailTemplate1 EmailTemplate { get; set; }
        [ForeignKey("SegmentID")]
        [InverseProperty("EmailCampaigns")]
        public virtual Segment Segment { get; set; }
    }
}
