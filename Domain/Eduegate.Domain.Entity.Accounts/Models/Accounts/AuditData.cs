using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Mutual;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("AuditData", Schema = "account")]
    public partial class AuditData
    {
        [Key]
        public long AuditDataID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ProcedureName { get; set; }

        public string ExportableExtension { get; set; }

        public bool IsExportable { get; set; }

        public string ReportName { get; set; }

        public string FilterParameters { get; set; }

        public string FType { get; set; }

    }
}