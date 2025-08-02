using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobsEntryHeadPayableMaps", Schema = "jobs")]
    public partial class JobsEntryHeadPayableMap
    {
        [Key]
        public long JobsEntryHeadPayableMapIID { get; set; }
        public long? PayableID { get; set; }
        public long? JobEntryHeadID { get; set; }

        [ForeignKey("JobEntryHeadID")]
        [InverseProperty("JobsEntryHeadPayableMaps")]
        public virtual JobEntryHead JobEntryHead { get; set; }
        [ForeignKey("PayableID")]
        [InverseProperty("JobsEntryHeadPayableMaps")]
        public virtual Payable Payable { get; set; }
    }
}
