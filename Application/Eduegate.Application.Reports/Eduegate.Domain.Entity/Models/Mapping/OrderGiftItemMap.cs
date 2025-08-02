using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderGiftItemMap : EntityTypeConfiguration<OrderGiftItem>
    {
        public OrderGiftItemMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderGiftItemID);

            // Properties
            this.Property(t => t.FreindName)
                .HasMaxLength(100);

            this.Property(t => t.FreindEmail)
                .HasMaxLength(225);

            this.Property(t => t.EmailMessage)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("OrderGiftItem");
            this.Property(t => t.OrderGiftItemID).HasColumnName("OrderGiftItemID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.FreindName).HasColumnName("FreindName");
            this.Property(t => t.FreindEmail).HasColumnName("FreindEmail");
            this.Property(t => t.EmailMessage).HasColumnName("EmailMessage");
        }
    }
}
