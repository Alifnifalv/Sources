using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassTeacherSubjectMapView
    {
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public int? ORDERNO { get; set; }
        [StringLength(4000)]
        public string ClassName { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(4000)]
        public string AcademicYear { get; set; }
        public long? ClassTeacherID { get; set; }
        [StringLength(555)]
        public string ClassTecherName { get; set; }
        public long? OtherTeacherID { get; set; }
        [StringLength(555)]
        public string OtherTecherName { get; set; }
        public long? ClassClassTeacherMapID { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
    }
}
