using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPromotionMap : EntityTypeConfiguration<ProductPromotion>
    {
        public ProductPromotionMap()
        {
            // Primary Key
            this.HasKey(t => t.PromotionID);

            // Properties
            this.Property(t => t.PromotionType)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ProductPromotion");
            this.Property(t => t.PromotionID).HasColumnName("PromotionID");
            this.Property(t => t.PromotionType).HasColumnName("PromotionType");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.PromoProductID).HasColumnName("PromoProductID");
        }
    }
}
