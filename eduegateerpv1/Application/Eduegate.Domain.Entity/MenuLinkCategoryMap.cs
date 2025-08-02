namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.MenuLinkCategoryMaps")]
    public partial class MenuLinkCategoryMap
    {
        [Key]
        public long MenuLinkCategoryMapIID { get; set; }

        public long? MenuLinkID { get; set; }

        public long? CategoryID { get; set; }

        public int? SortOrder { get; set; }

        [StringLength(500)]
        public string ActionLink { get; set; }

        public int? SiteID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Category Category { get; set; }

        public virtual MenuLink MenuLink { get; set; }

        public virtual Site Site { get; set; }
    }
}
