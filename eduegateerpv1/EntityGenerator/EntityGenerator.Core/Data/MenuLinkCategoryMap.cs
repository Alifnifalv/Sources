using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MenuLinkCategoryMaps", Schema = "catalog")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("MenuLinkCategoryMaps")]
        public virtual Category Category { get; set; }
        [ForeignKey("MenuLinkID")]
        [InverseProperty("MenuLinkCategoryMaps")]
        public virtual MenuLink MenuLink { get; set; }
        [ForeignKey("SiteID")]
        [InverseProperty("MenuLinkCategoryMaps")]
        public virtual Site Site { get; set; }
    }
}
