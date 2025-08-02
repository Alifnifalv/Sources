using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ParentPortalSchoolAcademicClassMap", Schema = "schools")]
    public partial class ParentPortalSchoolAcademicClassMap
    {
        [Key]
        public long ClassMapIID { get; set; }
        public int? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ClassID { get; set; }
    }
}
