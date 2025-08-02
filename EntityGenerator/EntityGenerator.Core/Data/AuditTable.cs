using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AuditTables", Schema = "account")]
    public partial class AuditTable
    {
        [Key]
        public long AuditTableID { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(255)]
        public string ProcedureName { get; set; }
    }
}
