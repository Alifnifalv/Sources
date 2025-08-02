using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class BranchInventory
    {
        [Key]
        public long? BranchID { get; set; }
        public string BranchName { get; set; }
        public long? Batch { get; set; }
        public decimal? Quantity { get; set; }
    }
}
