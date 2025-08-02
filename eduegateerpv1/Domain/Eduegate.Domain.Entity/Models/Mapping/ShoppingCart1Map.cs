using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCart1Map : EntityTypeConfiguration<ShoppingCart>
    {
        public ShoppingCart1Map()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartIID);

            // Properties
            this.Property(t => t.CartID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PaymentMethod)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ShoppingCarts", "inventory");
            this.Property(t => t.ShoppingCartIID).HasColumnName("ShoppingCartIID");
            this.Property(t => t.CartID).HasColumnName("CartID");
            this.Property(t => t.CartStatusID).HasColumnName("CartStatusID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
            this.Property(t => t.ShippingAddressID).HasColumnName("ShippingAddressID");
            this.Property(t => t.BillingAddressID).HasColumnName("BillingAddressID");
        }
    }
}
