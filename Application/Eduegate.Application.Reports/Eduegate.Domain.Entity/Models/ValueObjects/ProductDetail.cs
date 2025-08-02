using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Catalogs.Models.ValueObjects
{
    [NotMapped]
    public class ProductDetailValueObject
    {
        [Key]
        public long ProductSKUMapIID { get; set; }
        public bool IsActive { get; set; }
        public string ProductName { get; set; }
        public long ProductID { get; set; }
        public long SKUID { get; set; }
        public string BrandCode { get; set; }
        public long? BrandID { get; set; }
        public string CategoryCode { get; set; }
        public long? CategoryID { get; set; }
        public string AdditionalInfo1 { get; set; }
        public string AdditionalInfo2 { get; set; }
        public string ProductDescription { get; set; }
        public string AlertMessage { get; set; }
        public string ProductThumbnail { get; set; }
        public string ProductSmallImage { get; set; }
        public string ProductListingImage { get; set; }
        public string ProductLargeImage { get; set; }
    }
}
