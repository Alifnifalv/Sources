using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobsEntryHeadReceivableMaps", Schema = "jobs")]
    public partial class JobsEntryHeadReceivableMap
    {
        [Key]
        public long JobsEntryHeadReceivableMapIID { get; set; }
        public long? ReceivableID { get; set; }
        public long? JobEntryHeadID { get; set; }

        [ForeignKey("JobEntryHeadID")]
        [InverseProperty("JobsEntryHeadReceivableMaps")]
        public virtual JobEntryHead JobEntryHead { get; set; }
        [ForeignKey("ReceivableID")]
        [InverseProperty("JobsEntryHeadReceivableMaps")]
        public virtual Receivable Receivable { get; set; }
    }
}
