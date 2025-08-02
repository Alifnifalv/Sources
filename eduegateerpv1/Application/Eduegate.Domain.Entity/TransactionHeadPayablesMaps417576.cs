namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionHeadPayablesMaps417576")]
    public partial class TransactionHeadPayablesMaps417576
    {
        [Key]
        public long TransactionHeadPayablesMapIID { get; set; }

        public long? PayableID { get; set; }

        public long? HeadID { get; set; }
    }
}
