using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentsSummaryView
    {
        public byte? SchoolID { get; set; }
        public int? TotalStudents { get; set; }
        public int? Active { get; set; }
        public int? InActive { get; set; }
    }
}
