using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeQualificationMaps", Schema = "payroll")]
    public partial class EmployeeQualificationMap
    {
        public EmployeeQualificationMap()
        {
            QualificationCoreSubjectMaps = new HashSet<QualificationCoreSubjectMap>();
        }

        [Key]
        public long EmployeeQualificationMapIID { get; set; }
        public long? EmployeeID { get; set; }
        public byte? QualificationID { get; set; }
        [StringLength(50)]
        public string TitleOfProgramme { get; set; }
        [StringLength(50)]
        public string ModeOfProgramme { get; set; }
        [StringLength(50)]
        public string University { get; set; }
        public int? GraduationMonth { get; set; }
        public int? GraduationYear { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MarksInPercentage { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string Subject { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeQualificationMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("QualificationID")]
        [InverseProperty("EmployeeQualificationMaps")]
        public virtual Qualification Qualification { get; set; }
        [InverseProperty("EmployeeQualificationMap")]
        public virtual ICollection<QualificationCoreSubjectMap> QualificationCoreSubjectMaps { get; set; }
    }
}
