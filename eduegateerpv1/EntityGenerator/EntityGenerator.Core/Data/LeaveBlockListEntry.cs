using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeaveBlockListEntries", Schema = "payroll")]
    public partial class LeaveBlockListEntry
    {
        [Key]
        public long LeaveBlockListEntryIID { get; set; }
        public long? LeaveBlockListID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BlockDate { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("LeaveBlockListID")]
        [InverseProperty("LeaveBlockListEntries")]
        public virtual LeaveBlockList LeaveBlockList { get; set; }
    }
}
