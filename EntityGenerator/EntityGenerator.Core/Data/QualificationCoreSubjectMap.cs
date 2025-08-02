using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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

        [ForeignKey("EmployeeQualificationMapID")]
        [InverseProperty("QualificationCoreSubjectMaps")]
        public virtual EmployeeQualificationMap EmployeeQualificationMap { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("QualificationCoreSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}
