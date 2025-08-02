using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.CRM.Models
{
    [Table("Nationalities", Schema = "mutual")]
    public partial class Nationality
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nationality()
        {
            Leads = new HashSet<Lead>();
        }

        [Key]
        public int NationalityIID { get; set; }

        [StringLength(50)]
        public string NationalityName { get; set; }

        [StringLength(50)]
        public string NationalityCode { get; set; }

        public int? CountryID { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lead> Leads { get; set; }
    }
}