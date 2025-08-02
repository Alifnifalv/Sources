using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderSizeMap : EntityTypeConfiguration<OrderSize>
    {
        public OrderSizeMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderSizeID);

            // Properties
            this.Property(t => t.OrderSizeText)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OrderSize");
            this.Property(t => t.OrderSizeID).HasColumnName("OrderSizeID");
            this.Property(t => t.OrderSizeText).HasColumnName("OrderSizeText");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
