using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LibraryBookDetailsForIssue
    {
        [StringLength(50)]
        public string BookNumber { get; set; }
        public int? IssueCount { get; set; }
        public int? returnCount { get; set; }
        public int? NoRetrnCount { get; set; }
        public int? quatity { get; set; }
        [StringLength(20)]
        public string RackNumber { get; set; }
    }
}
