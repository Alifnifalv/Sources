namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ClassGroups")]
    public partial class ClassGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassGroup()
        {
            ClassClassGroupMaps = new HashSet<ClassClassGroupMap>();
            ClassGroupTeacherMaps = new HashSet<ClassGroupTeacherMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ClassGroupID { get; set; }

        public long? HeadTeacherID { get; set; }

        public string GroupDescription { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? SubjectID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassClassGroupMap> ClassClassGroupMaps { get; set; }

        public virtual School School { get; set; }

        public virtual Subject Subject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassGroupTeacherMap> ClassGroupTeacherMaps { get; set; }
    }
}
