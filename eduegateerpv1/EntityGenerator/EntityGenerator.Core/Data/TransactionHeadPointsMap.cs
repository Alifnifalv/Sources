using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionHeadPointsMap", Schema = "inventory")]
    public partial class TransactionHeadPointsMap
    {
        [Key]
        public long TransactionHeadPointsMapIID { get; set; }
        public long TransactionHeadID { get; set; }
        public long LoyaltyPoints { get; set; }
        public long CategorizationPoints { get; set; }

        [ForeignKey("TransactionHeadID")]
        [InverseProperty("TransactionHeadPointsMaps")]
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
