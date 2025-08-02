using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobsEntryHeadReceivableMaps", Schema = "jobs")]
    public partial class JobsEntryHeadReceivableMap
    {
        [Key]
        public long JobsEntryHeadReceivableMapIID { get; set; }
        public Nullable<long> ReceivableID { get; set; }
        public Nullable<long> JobEntryHeadID { get; set; }
        public Nullable<long> JobEntryDetailID { get; set; }
        public virtual Receivable Receivable { get; set; }
        public virtual JobEntryDetail JobEntryDetail { get; set; }
        public virtual JobEntryHead JobEntryHead { get; set; }
    }
}
