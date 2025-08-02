namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.SocialMediaPostingMaps")]
    public partial class SocialMediaPostingMap
    {
        [Key]
        public long SocialMediaPostingMapIID { get; set; }

        public long? SocialMediaPostingID { get; set; }

        public byte? SocialMediaID { get; set; }

        public virtual SocialMediaPosting SocialMediaPosting { get; set; }
    }
}
