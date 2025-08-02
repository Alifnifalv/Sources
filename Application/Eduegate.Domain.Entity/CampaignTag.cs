namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.CampaignTags")]
    public partial class CampaignTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CampaignTagIID { get; set; }

        [StringLength(50)]
        public string TagName { get; set; }
    }
}
