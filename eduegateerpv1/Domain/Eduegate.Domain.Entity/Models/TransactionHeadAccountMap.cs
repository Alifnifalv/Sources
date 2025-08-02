using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TransactionHeadAccountMaps", Schema = "inventory")]
    public partial class TransactionHeadAccountMap
    {
        [Key]
        public long TransactionHeadAccountMapIID { get; set; }
        public Nullable<long> TransactionHeadID { get; set; }
        public Nullable<long> AccountTransactionID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual AccountTransaction AccountTransaction { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
