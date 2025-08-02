using Eduegate.Domain.Entity.Models.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EntityGenerator.Core.Data;

namespace Eduegate.Domain.Entity.Models
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

        public decimal? MarksInPercentage { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set;
        //
        public string Subject { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Qualification Qualification { get; set; }

        public virtual ICollection<QualificationCoreSubjectMap> QualificationCoreSubjectMaps { get; set; }
    }
}




    