using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderItemCancelMap : EntityTypeConfiguration<OrderItemCancel>
    {
        public OrderItemCancelMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RefOrderID, t.RefOrderItemID, t.Quantity });

            // Properties
            this.Property(t => t.RefOrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefOrderItemID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Quantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("OrderItemCancel");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefOrderItemID).HasColumnName("RefOrderItemID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.UpdateInventory).HasColumnName("UpdateInventory");
        }
    }
}
