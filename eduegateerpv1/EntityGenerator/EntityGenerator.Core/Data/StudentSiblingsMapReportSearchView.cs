using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentSiblingsMapReportSearchView
    {
        public long? StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long? ApplicationID { get; set; }
        public long SiblingID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public long? ParentID { get; set; }
    }
}
