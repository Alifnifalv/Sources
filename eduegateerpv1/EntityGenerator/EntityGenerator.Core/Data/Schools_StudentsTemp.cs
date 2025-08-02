using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Schools.StudentsTemp")]
    public partial class Schools_StudentsTemp
    {
        [StringLength(20)]
        public string StudentCode { get; set; }
        [StringLength(50)]
        public string StudentName { get; set; }
        [StringLength(7)]
        public string Gender { get; set; }
        [StringLength(20)]
        public string ParentCode { get; set; }
    }
}
