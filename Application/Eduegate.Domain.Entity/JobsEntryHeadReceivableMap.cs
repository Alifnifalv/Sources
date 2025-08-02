namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobs.JobsEntryHeadReceivableMaps")]
    public partial class JobsEntryHeadReceivableMap
    {
        [Key]
        public long JobsEntryHeadReceivableMapIID { get; set; }

        public long? ReceivableID { get; set; }

        public long? JobEntryHeadID { get; set; }

        public virtual Receivable Receivable { get; set; }

        public virtual JobEntryHead JobEntryHead { get; set; }
    }
}
