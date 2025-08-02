using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ReturnMethods", Schema = "distribution")]
    public partial class ReturnMethod
    {
        public ReturnMethod()
        {
            Suppliers = new HashSet<Supplier>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public int ReturnMethodID { get; set; }
        [StringLength(100)]
        public string ReturnMethodName { get; set; }

        [InverseProperty("ReturnMethod")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [InverseProperty("ReturnMethod")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
