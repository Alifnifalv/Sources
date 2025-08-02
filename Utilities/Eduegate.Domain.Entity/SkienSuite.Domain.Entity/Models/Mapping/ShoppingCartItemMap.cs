using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartItemMap : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartItemID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ShoppingCartItems", "inventory");
            this.Property(t => t.ShoppingCartItemID).HasColumnName("ShoppingCartItemID");
            this.Property(t => t.ShoppingCartID).HasColumnName("ShoppingCartID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.NetWeight).HasColumnName("NetWeight");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.CostPrice).HasColumnName("CostPrice");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.DisplayRange).HasColumnName("DisplayRange");

            // Relationships
            this.HasOptional(t => t.ShoppingCart)
                .WithMany(t => t.ShoppingCartItems)
                .HasForeignKey(d => d.ShoppingCartID);

        }
    }
}
