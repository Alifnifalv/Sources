using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AccountTransactionHeadAccountMaps", Schema = "account")]
    public partial class AccountTransactionHeadAccountMap
    {
        [Key]
        public long AccountTransactionHeadAccountMapIID { get; set; }
        public long AccountTransactionID { get; set; }
        public long AccountTransactionHeadID { get; set; }

        [ForeignKey("AccountTransactionID")]
        [InverseProperty("AccountTransactionHeadAccountMaps")]
        public virtual AccountTransaction AccountTransaction { get; set; }
        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("AccountTransactionHeadAccountMaps")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
    }
}
