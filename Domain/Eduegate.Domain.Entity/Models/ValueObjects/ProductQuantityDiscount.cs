using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    [Keyless]
    public class ProductQuantityDiscount
    {
        public string DiscountPercentage { get; set; }
        public string Quantity { get; set; }
        public string QtyPrice { get; set; }
    }
}
