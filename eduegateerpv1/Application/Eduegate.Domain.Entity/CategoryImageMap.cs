namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.CategoryImageMaps")]
    public partial class CategoryImageMap
    {
        [Key]
        public long CategoryImageMapIID { get; set; }

        public long? CategoryID { get; set; }

        public byte? ImageTypeID { get; set; }

        [StringLength(200)]
        public string ImageFile { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [Column(TypeName = "text")]
        public string ImageTitle { get; set; }

        [StringLength(400)]
        public string ImageLink { get; set; }

        [StringLength(200)]
        public string ImageTarget { get; set; }

        public int? ActionLinkTypeID { get; set; }

        [StringLength(300)]
        public string ImageLinkParameters { get; set; }

        public long? SerialNo { get; set; }

        public int? CompanyID { get; set; }

        public int? SiteID { get; set; }

        public virtual Category Category { get; set; }

        public virtual ImageType ImageType { get; set; }
    }
}
