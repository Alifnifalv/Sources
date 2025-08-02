using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Accounts.Models.Accounts
{
    [Table("AccountTransactionHeadAccountMaps", Schema = "account")]
    public partial class AccountTransactionHeadAccountMap
    {
        [Key]
        public long AccountTransactionHeadAccountMapIID { get; set; }

        public long AccountTransactionID { get; set; }

        public long AccountTransactionHeadID { get; set; }

        public virtual AccountTransaction AccountTransaction { get; set; }

        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
    }
}