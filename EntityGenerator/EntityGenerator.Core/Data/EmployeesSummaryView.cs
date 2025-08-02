using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeesSummaryView
    {
        public int? TotalEmployees { get; set; }
        public int? MaleStaff { get; set; }
        public int? FemaleStaff { get; set; }
    }
}
