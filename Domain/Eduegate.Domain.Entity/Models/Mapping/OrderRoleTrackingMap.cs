using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderRoleTrackingMap : EntityTypeConfiguration<OrderRoleTracking>
    {
        public OrderRoleTrackingMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderTrackingIID);

            // Properties
            this.Property(t => t.DevicePlatform)
                .HasMaxLength(150);

            this.Property(t => t.DeviceVersion)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("OrderRoleTracking", "orders");
            this.Property(t => t.OrderTrackingIID).HasColumnName("OrderTrackingIID");
            this.Property(t => t.TransactionHeadID).HasColumnName("TransactionHeadID");
            this.Property(t => t.DevicePlatform).HasColumnName("DevicePlatform");
            this.Property(t => t.DeviceVersion).HasColumnName("DeviceVersion");

            // Relationships
            this.HasRequired(t => t.TransactionHead)
                .WithMany(t => t.OrderRoleTrackings)
                .HasForeignKey(d => d.TransactionHeadID);

        }
    }
}
