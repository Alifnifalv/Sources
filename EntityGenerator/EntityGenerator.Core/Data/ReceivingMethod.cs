using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ReceivingMethods", Schema = "distribution")]
    public partial class ReceivingMethod
    {
        public ReceivingMethod()
        {
            Suppliers = new HashSet<Supplier>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public int ReceivingMethodID { get; set; }
        [StringLength(100)]
        public string ReceivingMethodName { get; set; }

        [InverseProperty("ReceivingMethod")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [InverseProperty("ReceivingMethod")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
