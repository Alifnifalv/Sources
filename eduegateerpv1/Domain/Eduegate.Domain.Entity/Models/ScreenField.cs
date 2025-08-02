namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ScreenFields", Schema = "setting")]
    public partial class ScreenField
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScreenField()
        {
            ScreenFieldSettings = new HashSet<ScreenFieldSetting>();
            UserScreenFieldSettings = new HashSet<UserScreenFieldSetting>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ScreenFieldID { get; set; }

        [StringLength(100)]
        public string FieldName { get; set; }

        [StringLength(100)]
        public string ModelName { get; set; }

        [StringLength(100)]
        public string PhysicalFieldName { get; set; }

        [StringLength(50)]
        public string LookupName { get; set; }

        [StringLength(50)]
        public string DateType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenFieldSetting> ScreenFieldSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserScreenFieldSetting> UserScreenFieldSettings { get; set; }
    }
}
