using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SocialMediaPostingMaps", Schema = "marketing")]
    public partial class SocialMediaPostingMap
    {
        [Key]
        public long SocialMediaPostingMapIID { get; set; }
        public long? SocialMediaPostingID { get; set; }
        public byte? SocialMediaID { get; set; }

        [ForeignKey("SocialMediaPostingID")]
        [InverseProperty("SocialMediaPostingMaps")]
        public virtual SocialMediaPosting SocialMediaPosting { get; set; }
    }
}
