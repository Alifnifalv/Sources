using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SchoolWise_TeachersRatio
    {
        [StringLength(50)]
        public string SchoolName { get; set; }
        public double? TeacherRatio { get; set; }
        public double? TeacherCount { get; set; }
    }
}
