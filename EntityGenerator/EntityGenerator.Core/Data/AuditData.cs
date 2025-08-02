using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AuditData", Schema = "account")]
    public partial class AuditData
    {
        [Key]
        public long AuditDataID { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(255)]
        public string ProcedureName { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string ExportableExtension { get; set; }
        public bool? IsExportable { get; set; }
        [StringLength(255)]
        public string ReportName { get; set; }
        public string FilterParameters { get; set; }
        [StringLength(255)]
        public string FType { get; set; }
    }
}
