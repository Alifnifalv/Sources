using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ArchiveTables", Schema = "setting")]
    public partial class ArchiveTable
    {
        [Key]
        public int ArchiveTableID { get; set; }
        [Required]
        public string TableName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastArchiveDate { get; set; }
        public int? LastArchiveID { get; set; }
        [Required]
        public string TableIDParameter { get; set; }
        [Required]
        public string DateParameter { get; set; }
    }
}
