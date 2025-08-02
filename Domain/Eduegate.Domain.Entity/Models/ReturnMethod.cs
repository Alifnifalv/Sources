using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ReturnMethods", Schema = "distribution")]
    public partial class ReturnMethod
    {
        public ReturnMethod()
        {
            this.Suppliers = new List<Supplier>();
        }

        [Key]
        public int ReturnMethodID { get; set; }
        public string ReturnMethodName { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
