using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartItemLockedMap : EntityTypeConfiguration<ShoppingCartItemLocked>
    {
        public ShoppingCartItemLockedMap()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartItemID);

            // Properties
            this.Property(t => t.CustomerSessionID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ShoppingCartItemLocked");
            this.Property(t => t.ShoppingCartItemID).HasColumnName("ShoppingCartItemID");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
