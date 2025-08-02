using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentLanguageProficiencyView
    {
        [Required]
        [StringLength(14)]
        [Unicode(false)]
        public string Head { get; set; }
        [StringLength(500)]
        public string Data { get; set; }
        public long StudentID { get; set; }
    }
}
