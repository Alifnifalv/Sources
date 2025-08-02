using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MenuLinks", Schema = "setting")]
    [Index("MenuLinkTypeID", Name = "IDX_MenuLinks_MenuLinkTypeID_ParentMenuID__MenuName")]
    [Index("MenuName", Name = "IDX_MenuLinks_MenuName_")]
    [Index("ParentMenuID", "MenuLinkTypeID", Name = "idx_MenuLinksParentMenuIDMenuLinkTypeID")]
    public partial class MenuLink
    {
        public MenuLink()
        {
            MenuLinkBrandMaps = new HashSet<MenuLinkBrandMap>();
            MenuLinkCategoryMaps = new HashSet<MenuLinkCategoryMap>();
            MenuLinkCultureDatas = new HashSet<MenuLinkCultureData>();
        }

        [Key]
        public long MenuLinkIID { get; set; }
        public long? ParentMenuID { get; set; }
        [StringLength(100)]
        public string MenuName { get; set; }
        public byte? MenuLinkTypeID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ActionType { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string ActionLink { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string ActionLink1 { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string ActionLink2 { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string ActionLink3 { get; set; }
        [StringLength(1000)]
        [Unicode(false)]
        public string Parameters { get; set; }
        public int? SortOrder { get; set; }
        [StringLength(100)]
        public string MenuTitle { get; set; }
        [StringLength(50)]
        public string MenuIcon { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public int? SiteID { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string MenuGroup { get; set; }
        public bool? IsHidden { get; set; }

        [ForeignKey("MenuLinkTypeID")]
        [InverseProperty("MenuLinks")]
        public virtual MenuLinkType MenuLinkType { get; set; }
        [InverseProperty("MenuLink")]
        public virtual ICollection<MenuLinkBrandMap> MenuLinkBrandMaps { get; set; }
        [InverseProperty("MenuLink")]
        public virtual ICollection<MenuLinkCategoryMap> MenuLinkCategoryMaps { get; set; }
        [InverseProperty("MenuLink")]
        public virtual ICollection<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }
    }
}
