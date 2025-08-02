namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.AccountTransactionHeadAccountMaps")]
    public partial class AccountTransactionHeadAccountMap
    {
        [Key]
        public long AccountTransactionHeadAccountMapIID { get; set; }

        public long AccountTransactionID { get; set; }

        public long AccountTransactionHeadID { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual AccountTransaction AccountTransaction { get; set; }
    }
}
