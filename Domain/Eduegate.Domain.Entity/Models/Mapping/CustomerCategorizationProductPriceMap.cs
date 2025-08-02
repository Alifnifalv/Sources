using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerCategorizationProductPriceMap : EntityTypeConfiguration<CustomerCategorizationProductPrice>
    {
        public CustomerCategorizationProductPriceMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CustomerCategorizationProductPrice");
            this.Property(t => t.ProductPriceID).HasColumnName("ProductPriceID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.RefSlabID).HasColumnName("RefSlabID");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.OriginalPrice).HasColumnName("OriginalPrice");
            this.Property(t => t.AppliedDiscount).HasColumnName("AppliedDiscount");
        }
    }
}
