using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ReceivingMethods", Schema = "distribution")]
    public partial class ReceivingMethod
    {
        public ReceivingMethod()
        {
            this.Suppliers = new List<Supplier>();
        }

        [Key]
        public int ReceivingMethodID { get; set; }
        public string ReceivingMethodName { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
