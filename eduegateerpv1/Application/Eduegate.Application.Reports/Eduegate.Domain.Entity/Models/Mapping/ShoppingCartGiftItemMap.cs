using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartGiftItemMap : EntityTypeConfiguration<ShoppingCartGiftItem>
    {
        public ShoppingCartGiftItemMap()
        {
            // Primary Key
            this.HasKey(t => t.SCGistItemID);

            // Properties
            this.Property(t => t.CustomerSessionID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FreindName)
                .HasMaxLength(100);

            this.Property(t => t.FreindEmail)
                .HasMaxLength(225);

            this.Property(t => t.EmailMessage)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("ShoppingCartGiftItem");
            this.Property(t => t.SCGistItemID).HasColumnName("SCGistItemID");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.FreindName).HasColumnName("FreindName");
            this.Property(t => t.FreindEmail).HasColumnName("FreindEmail");
            this.Property(t => t.EmailMessage).HasColumnName("EmailMessage");
        }
    }
}
