namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AccountTransactionHeadAccountMaps", Schema = "account")]
    public partial class AccountTransactionHeadAccountMap
    {
        [Key]
        public long AccountTransactionHeadAccountMapIID { get; set; }

        public long AccountTransactionID { get; set; }

        public long AccountTransactionHeadID { get; set; }

        public long? TransactionHeadID { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }

        public virtual AccountTransaction AccountTransaction { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
