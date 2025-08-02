using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartTransLockedMap : EntityTypeConfiguration<ShoppingCartTransLocked>
    {
        public ShoppingCartTransLockedMap()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartItemID);

            // Properties
            this.Property(t => t.CustomerSessionID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ShoppingCartTransLocked");
            this.Property(t => t.ShoppingCartItemID).HasColumnName("ShoppingCartItemID");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.PreviousTotal).HasColumnName("PreviousTotal");
            this.Property(t => t.CartTotal).HasColumnName("CartTotal");
            this.Property(t => t.MaxLimit).HasColumnName("MaxLimit");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
