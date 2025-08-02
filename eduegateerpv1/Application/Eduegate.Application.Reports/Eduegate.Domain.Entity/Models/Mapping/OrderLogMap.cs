using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderLogMap : EntityTypeConfiguration<OrderLog>
    {
        public OrderLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.RefLogOrderID).HasColumnName("RefLogOrderID");
            this.Property(t => t.OrderStatus).HasColumnName("OrderStatus");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.LogDateTime).HasColumnName("LogDateTime");

            // Relationships
            this.HasRequired(t => t.OrderMaster)
                .WithMany(t => t.OrderLogs)
                .HasForeignKey(d => d.RefLogOrderID);

        }
    }
}
