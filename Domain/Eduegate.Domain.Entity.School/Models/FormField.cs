using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("FormFields", Schema = "form")]
    public partial class FormField
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormField()
        {
            FormValues = new HashSet<FormValue>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FormFieldID { get; set; }

        public int? FormID { get; set; }

        public string FieldName { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsTitle { get; set; }

        public bool? IsSubTitle { get; set; }

        public virtual Form Form { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormValue> FormValues { get; set; }
    }
}