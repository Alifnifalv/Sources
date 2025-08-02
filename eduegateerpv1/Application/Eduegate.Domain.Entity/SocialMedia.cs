namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.SocialMedias")]
    public partial class SocialMedia
    {
        public byte SocialMediaID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string ImagePath { get; set; }

        [StringLength(500)]
        public string ActionUrl { get; set; }

        [StringLength(50)]
        public string CssStyles { get; set; }

        public bool? IsStatus { get; set; }
    }
}
