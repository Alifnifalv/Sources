using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Class_Section_Weeekly_Period_11_12
    {
        public double? Sl { get; set; }
        [StringLength(255)]
        public string ClassName { get; set; }
        [StringLength(255)]
        public string SectionName { get; set; }
        [StringLength(255)]
        public string Subject { get; set; }
        public double? Periods_Week { get; set; }
        [Unicode(false)]
        public string ClassIDs { get; set; }
        [Unicode(false)]
        public string SectionIDs { get; set; }
        [Unicode(false)]
        public string SubjectIDs { get; set; }
        public int? SubjectTypeID { get; set; }
    }
}
