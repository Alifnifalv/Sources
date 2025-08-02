namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.RemarksEntryStudentMaps")]
    public partial class RemarksEntryStudentMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RemarksEntryStudentMap()
        {
            RemarksEntryExamMaps = new HashSet<RemarksEntryExamMap>();
        }

        [Key]
        public long RemarksEntryStudentMapIID { get; set; }

        public long? RemarksEntryID { get; set; }

        public long? StudentID { get; set; }

        public string Remarks1 { get; set; }

        public string Remarks2 { get; set; }

        public virtual RemarksEntry RemarksEntry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemarksEntryExamMap> RemarksEntryExamMaps { get; set; }

        public virtual Student Student { get; set; }
    }
}
