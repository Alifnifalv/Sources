using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderOfflineLogMap : EntityTypeConfiguration<OrderOfflineLog>
    {
        public OrderOfflineLogMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderOfflineLogID);

            // Properties
            this.Property(t => t.OrderStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("OrderOfflineLog");
            this.Property(t => t.OrderOfflineLogID).HasColumnName("OrderOfflineLogID");
            this.Property(t => t.RefOrderOfflineID).HasColumnName("RefOrderOfflineID");
            this.Property(t => t.OrderStatus).HasColumnName("OrderStatus");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.LogDateTime).HasColumnName("LogDateTime");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
