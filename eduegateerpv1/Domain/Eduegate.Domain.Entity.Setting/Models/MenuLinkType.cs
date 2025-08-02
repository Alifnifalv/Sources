namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("MenuLinkTypes", Schema = "setting")]
    public partial class MenuLinkType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuLinkType()
        {
            MenuLinks = new HashSet<MenuLink>();
        }
        [Key]
        public byte MenuLinkTypeID { get; set; }

        [StringLength(100)]
        public string MenuLinkTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLink> MenuLinks { get; set; }
    }
}
