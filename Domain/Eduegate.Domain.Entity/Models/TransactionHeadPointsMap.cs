using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TransactionHeadPointsMap", Schema = "inventory")]
    public partial class TransactionHeadPointsMap
    {
        [Key]
        public long TransactionHeadPointsMapIID { get; set; }
        public long TransactionHeadID { get; set; }
        public long LoyaltyPoints { get; set; }
        public long CategorizationPoints { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
