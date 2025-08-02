using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartItem1Map : EntityTypeConfiguration<ShoppingCartItem1>
    {
        public ShoppingCartItem1Map()
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

            // Relationships
            //this.HasOptional(t => t.ShoppingCart)
            //    .WithMany(t => t.ShoppingCartItems)
            //    .HasForeignKey(d => d.ShoppingCartID);

        }
    }
}
