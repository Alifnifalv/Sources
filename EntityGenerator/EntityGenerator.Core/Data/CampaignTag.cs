using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CampaignTags", Schema = "marketing")]
    public partial class CampaignTag
    {
        [Key]
        public long CampaignTagIID { get; set; }
        [StringLength(50)]
        public string TagName { get; set; }
    }
}
