namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("form.Forms")]
    public partial class Form
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Form()
        {
            FormFields = new HashSet<FormField>();
            FormValues = new HashSet<FormValue>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FormID { get; set; }

        public string FormName { get; set; }

        [StringLength(100)]
        public string ReportName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormField> FormFields { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FormValue> FormValues { get; set; }
    }
}
