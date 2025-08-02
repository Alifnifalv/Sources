using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassCoordinators", Schema = "schools")]
    public partial class ClassCoordinator
    {
        public ClassCoordinator()
        {
            ClassCoordinatorClassMaps = new HashSet<ClassCoordinatorClassMap>();
        }

        [Key]
        public long ClassCoordinatorIID { get; set; }
        public long? CoordinatorID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? ISACTIVE { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ClassCoordinators")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("CoordinatorID")]
        [InverseProperty("ClassCoordinators")]
        public virtual Employee Coordinator { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassCoordinators")]
        public virtual School School { get; set; }
        [InverseProperty("ClassCoordinator")]
        public virtual ICollection<ClassCoordinatorClassMap> ClassCoordinatorClassMaps { get; set; }
    }
}
