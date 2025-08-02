using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderStatusMasterMap : EntityTypeConfiguration<OrderStatusMaster>
    {
        public OrderStatusMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderStatusID);

            // Properties
            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StatusText)
                .HasMaxLength(50);

            this.Property(t => t.StatusTextAr)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OrderStatusMaster");
            this.Property(t => t.OrderStatusID).HasColumnName("OrderStatusID");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusText).HasColumnName("StatusText");
            this.Property(t => t.StatusTextAr).HasColumnName("StatusTextAr");
        }
    }
}
