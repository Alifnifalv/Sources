namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompanyGroups", Schema = "mutual")]
    public partial class CompanyGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CompanyGroup()
        {
            Companies = new HashSet<Company>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyGroupID { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company> Companies { get; set; }
    }
}
