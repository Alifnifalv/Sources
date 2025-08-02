namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.EmailCampaigns")]
    public partial class EmailCampaign
    {
        [Key]
        public long EmailCampaignIID { get; set; }

        public long? CampaignID { get; set; }

        public long? SegmentID { get; set; }

        public long? EmailTemplateID { get; set; }

        public virtual Campaign Campaign { get; set; }

        public virtual EmailTemplates1 EmailTemplates1 { get; set; }

        public virtual Segment Segment { get; set; }
    }
}
