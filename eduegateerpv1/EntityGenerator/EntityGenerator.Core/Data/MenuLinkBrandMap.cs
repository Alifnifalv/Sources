using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MenuLinkBrandMaps", Schema = "catalog")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BrandID")]
        [InverseProperty("MenuLinkBrandMaps")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("MenuLinkID")]
        [InverseProperty("MenuLinkBrandMaps")]
        public virtual MenuLink MenuLink { get; set; }
    }
}
