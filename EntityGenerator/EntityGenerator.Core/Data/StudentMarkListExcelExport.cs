using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentMarkListExcelExport", Schema = "schools")]
    public partial class StudentMarkListExcelExport
    {
        [Key]
        public long MarkListDataIID { get; set; }
        [StringLength(100)]
        public string AdmissionNumber { get; set; }
        [StringLength(100)]
        public string StudentName { get; set; }
        [StringLength(100)]
        public string SubjectName { get; set; }
        [StringLength(100)]
        public string ExamName { get; set; }
        [StringLength(100)]
        public string Mark { get; set; }
        public int? OrderNo { get; set; }
        [StringLength(20)]
        public string ClassName { get; set; }
        [StringLength(20)]
        public string SectionName { get; set; }
    }
}
