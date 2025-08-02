using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassSubjectMapSearch
    {
        public long? ClassSubjectMapIID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? ClassID { get; set; }
        public byte? SchoolID { get; set; }
        public int? SectionCount { get; set; }
        [StringLength(4000)]
        public string Section { get; set; }
        public int? NumOfSubjects { get; set; }
        [StringLength(4000)]
        public string Subjects { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
