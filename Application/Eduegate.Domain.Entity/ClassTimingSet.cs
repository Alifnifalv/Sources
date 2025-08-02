namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassTimingSets")]
    public partial class ClassTimingSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassTimingSet()
        {
            ClassTimings = new HashSet<ClassTiming>();
            WeekDays = new HashSet<WeekDay>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassTimingSetID { get; set; }

        [StringLength(50)]
        public string TimingSetName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassTiming> ClassTimings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WeekDay> WeekDays { get; set; }
    }
}
