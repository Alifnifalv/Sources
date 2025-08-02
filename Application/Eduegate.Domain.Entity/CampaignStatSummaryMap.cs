namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.CampaignStatSummaryMaps")]
    public partial class CampaignStatSummaryMap
    {
        [Key]
        public long CampaignStatSummaryMapIID { get; set; }

        public int? CampaignStatTypeID { get; set; }

        public long? CampaignID { get; set; }

        public decimal? Value { get; set; }

        public virtual CampaignStatType CampaignStatType { get; set; }

        public virtual SocailMediaCampaign SocailMediaCampaign { get; set; }
    }
}
