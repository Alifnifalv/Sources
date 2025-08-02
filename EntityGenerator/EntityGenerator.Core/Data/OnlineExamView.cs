using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class OnlineExamView
    {
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public long CandidateIID { get; set; }
        public byte? SubjectTypeID { get; set; }
        [StringLength(250)]
        public string remarks { get; set; }
    }
}
