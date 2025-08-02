using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TimeTableSearch
    {
        [StringLength(50)]
        public string Class { get; set; }
        [StringLength(50)]
        public string Section { get; set; }
        [StringLength(100)]
        public string Employee { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? EmployeeIID { get; set; }
    }
}
