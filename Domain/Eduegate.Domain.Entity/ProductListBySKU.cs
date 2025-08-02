using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public class ProductListBySKU
    {
        public long ProductIID { get; set; }
        [Key]
        public long ProductSKUMapIID { get; set; }
        public decimal? ProductPrice { get; set; }
        public int Sequence { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public string PartNo { get; set; }
        public string SKU { get; set; }
        public string ImageFile { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? SellingQuantityLimit { get; set; }
    }
}