using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ClassCoordinators", Schema = "schools")]
    public partial class ClassCoordinator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassCoordinator()
        {
            ClassCoordinatorClassMaps = new HashSet<ClassCoordinatorClassMap>();
        }

        [Key]
        public long ClassCoordinatorIID { get; set; }

        public long? CoordinatorID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public bool? ISACTIVE { get; set; }

        public long? HeadMasterID { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassCoordinatorClassMap> ClassCoordinatorClassMaps { get; set; }

        public virtual Schools School { get; set; }
    }
}
