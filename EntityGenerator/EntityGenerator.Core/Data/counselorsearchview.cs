using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class counselorsearchview
    {
        public long SignupIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(555)]
        public string Student { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
