using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartMap : EntityTypeConfiguration<ShoppingCart>
    {
        public ShoppingCartMap()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartItemID);

            // Properties
            this.Property(t => t.Price)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ShoppingCart", "catalog");
            this.Property(t => t.ShoppingCartItemID).HasColumnName("ShoppingCartItemID");
            this.Property(t => t.SkuID).HasColumnName("SkuID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.DeliveryID).HasColumnName("DeliveryID");
        }
    }
}
