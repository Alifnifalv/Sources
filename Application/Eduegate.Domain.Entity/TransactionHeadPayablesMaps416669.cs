namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionHeadPayablesMaps416669")]
    public partial class TransactionHeadPayablesMaps416669
    {
        [Key]
        public long TransactionHeadPayablesMapIID { get; set; }

        public long? PayableID { get; set; }

        public long? HeadID { get; set; }
    }
}
