using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentsLeaveRequestListForDashBoard
    {
        public long ReferenceIID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string Date { get; set; }
        [StringLength(555)]
        public string Title { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public string SubTitle { get; set; }
    }
}
