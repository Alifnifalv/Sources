using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderTrackingMap : EntityTypeConfiguration<OrderTracking>
    {
        public OrderTrackingMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderTrackingIID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("OrderTracking", "orders");
            this.Property(t => t.OrderTrackingIID).HasColumnName("OrderTrackingIID");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.StatusDate).HasColumnName("StatusDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.OrderTrackings)
                .HasForeignKey(d => d.OrderID);
            this.HasOptional(t => t.TransactionStatus)
                .WithMany(t => t.OrderTrackings)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
