using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobsEntryHeadPayableMaps", Schema = "jobs")]
    public partial class JobsEntryHeadPayableMap
    {
        [Key]
        public long JobsEntryHeadPayableMapIID { get; set; }
        public Nullable<long> PayableID { get; set; }
        public Nullable<long> JobEntryHeadID { get; set; }
        public virtual Payable Payable { get; set; }
        public virtual JobEntryHead JobEntryHead { get; set; }
    }
}
