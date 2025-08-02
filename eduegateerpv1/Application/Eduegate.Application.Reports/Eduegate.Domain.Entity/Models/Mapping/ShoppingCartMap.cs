using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartMap: EntityTypeConfiguration<ShoppingCart>
    {
        public ShoppingCartMap()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartIID);

            // Properties
            this.Property(t => t.CartID)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ShoppingCarts", "inventory");
            this.Property(t => t.ShoppingCartIID).HasColumnName("ShoppingCartIID");
            this.Property(t => t.CartID).HasColumnName("CartID");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
            this.Property(t => t.CartStatusID).HasColumnName("CartStatusID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.ShippingAddressID).HasColumnName("ShippingAddressID");
            this.Property(t => t.BillingAddressID).HasColumnName("BillingAddressID");
            this.Property(t => t.IsInventoryBlocked).HasColumnName("IsInventoryBlocked");
            this.Property(t => t.InventoryBlockedDateTime).HasColumnName("InventoryBlockedDateTime");
            this.Property(t => t.BlockedHeadID).HasColumnName("BlockedHeadID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.PaymentGateWayID).HasColumnName("PaymentGateWayID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.IsInternational).HasColumnName("IsInternational");
        }
    }
}
