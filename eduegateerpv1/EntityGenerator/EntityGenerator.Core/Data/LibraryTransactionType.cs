using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryTransactionTypes", Schema = "schools")]
    public partial class LibraryTransactionType
    {
        public LibraryTransactionType()
        {
            LibraryTransactions = new HashSet<LibraryTransaction>();
        }

        [Key]
        public byte LibraryTransactionTypeID { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }

        [InverseProperty("LibraryTransactionType")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
    }
}
