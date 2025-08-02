namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.MenuLinkBrandMaps")]
    public partial class MenuLinkBrandMap
    {
        [Key]
        public long MenuLinkBrandMapIID { get; set; }

        public long? MenuLinkID { get; set; }

        public long? BrandID { get; set; }

        [StringLength(500)]
        public string ActionLink { get; set; }

        public int? SortOrder { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual MenuLink MenuLink { get; set; }
    }
}
