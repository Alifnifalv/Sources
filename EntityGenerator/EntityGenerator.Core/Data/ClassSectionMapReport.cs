using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassSectionMapReport
    {
        public int ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? NumOfSections { get; set; }
        [StringLength(4000)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string School { get; set; }
        [StringLength(20)]
        public string AcademicYear { get; set; }
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
