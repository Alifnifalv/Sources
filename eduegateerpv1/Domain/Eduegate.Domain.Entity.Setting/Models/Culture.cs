namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Cultures", Schema = "mutual")]
    public partial class Culture
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Culture()
        {
            MenuLinkCultureDatas = new HashSet<MenuLinkCultureData>();
        }
        [Key]
        public byte CultureID { get; set; }

        [StringLength(50)]
        public string CultureCode { get; set; }

        [StringLength(100)]
        public string CultureName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MenuLinkCultureData> MenuLinkCultureDatas { get; set; }
    }
}
