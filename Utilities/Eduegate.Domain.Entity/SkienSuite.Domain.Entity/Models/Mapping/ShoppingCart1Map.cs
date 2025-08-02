using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCart1Map : EntityTypeConfiguration<ShoppingCart1>
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
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.CurrencyCode)
                .HasMaxLength(50);

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
            this.Property(t => t.IsInventoryBlocked).HasColumnName("IsInventoryBlocked");
            this.Property(t => t.InventoryBlockedDateTime).HasColumnName("InventoryBlockedDateTime");
            this.Property(t => t.BlockedHeadID).HasColumnName("BlockedHeadID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.PaymentGateWayID).HasColumnName("PaymentGateWayID");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.IsInternational).HasColumnName("IsInternational");

            // Relationships
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.ShoppingCarts)
                .HasForeignKey(d => d.BlockedHeadID);

        }
    }
}
