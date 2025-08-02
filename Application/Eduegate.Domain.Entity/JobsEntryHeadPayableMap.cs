namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("jobs.JobsEntryHeadPayableMaps")]
    public partial class JobsEntryHeadPayableMap
    {
        [Key]
        public long JobsEntryHeadPayableMapIID { get; set; }

        public long? PayableID { get; set; }

        public long? JobEntryHeadID { get; set; }

        public virtual Payable Payable { get; set; }

        public virtual JobEntryHead JobEntryHead { get; set; }
    }
}
