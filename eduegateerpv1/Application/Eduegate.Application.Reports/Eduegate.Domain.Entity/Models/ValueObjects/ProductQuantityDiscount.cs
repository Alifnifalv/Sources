using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class ProductQuantityDiscount
    {
        public string DiscountPercentage { get; set; }
        public string Quantity { get; set; }
        public string QtyPrice { get; set; }
    }
}
