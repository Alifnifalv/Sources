using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductQuantityDiscountMap : EntityTypeConfiguration<ProductQuantityDiscount>
    {
        public ProductQuantityDiscountMap()
        {
            // Primary Key
            this.HasKey(t => t.DiscountID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductQuantityDiscount");
            this.Property(t => t.DiscountID).HasColumnName("DiscountID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
