using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ApplicationFormSearchView
    {
        public int? FormID { get; set; }
        public string FormName { get; set; }
        [StringLength(100)]
        public string ReportName { get; set; }
        public long? ReferenceIID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
