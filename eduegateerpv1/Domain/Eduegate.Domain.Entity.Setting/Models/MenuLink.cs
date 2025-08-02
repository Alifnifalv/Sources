namespace Eduegate.Domain.Entity.Setting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("MenuLinks", Schema = "setting")]
    public partial class MenuLink
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuLink()
        {
            MenuLinkCultureDatas = new HashSet<MenuLinkCultureData>();
            MenuLinks1 = new HashSet<MenuLink>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long MenuLinkIID { get; set; }

        public long? ParentMenuID { get; set; }

        [StringLength(100)]
        public string MenuName { get; set; }

        public byte? MenuLinkTypeID { get; set; }

        [StringLength(10)]
        public string ActionType { get; set; }

        [StringLength(500)]
        public string ActionLink { get; set; }

        [StringLength(500)]
        public string ActionLink1 { get; set; }

        [StringLength(500)]
        public string ActionLink2 { get; set; }

        [StringLength(500)]
        public string ActionLink3 { get; set; }

        [StringLength(1000)]
        public string Parameters { get; set; }

        public int? SortOrder { get; set; }

        [StringLength(100)]
        public string MenuTitle { get; set; }

        [StringLength(50)]
        public string MenuIcon { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public int? SiteID { get; set; }

        [StringLength(20)]
        public string MenuGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLink> MenuLinks1 { get; set; }

        public virtual MenuLink MenuLink1 { get; set; }

        public virtual MenuLinkType MenuLinkType { get; set; }
    }
}
