using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Eduegate.Domain.Entity.Models;

namespace EntityGenerator.Core.Data
{
    [Table("QualificationCoreSubjectMaps", Schema = "payroll")]
    public partial class QualificationCoreSubjectMap
    {
        [Key]
        public long QualificationCoreSubjectMapIID { get; set; }
        public long? EmployeeQualificationMapID { get; set; }
        public int? SubjectID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        public virtual EmployeeQualificationMap EmployeeQualificationMap { get; set; }

    }
}
