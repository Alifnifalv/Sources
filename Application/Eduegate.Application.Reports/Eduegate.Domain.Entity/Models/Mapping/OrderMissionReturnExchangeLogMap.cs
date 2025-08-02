using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMissionReturnExchangeLogMap : EntityTypeConfiguration<OrderMissionReturnExchangeLog>
    {
        public OrderMissionReturnExchangeLogMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderMissionReturnExchangeLogID);

            // Properties
            this.Property(t => t.OrderMissionReturnExchangeStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("OrderMissionReturnExchangeLog");
            this.Property(t => t.OrderMissionReturnExchangeLogID).HasColumnName("OrderMissionReturnExchangeLogID");
            this.Property(t => t.OrderMissonReturnExchangeMasterID).HasColumnName("OrderMissonReturnExchangeMasterID");
            this.Property(t => t.OrderMissionReturnExchangeStatus).HasColumnName("OrderMissionReturnExchangeStatus");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.OrderMissionReturnExchangeLogDateTime).HasColumnName("OrderMissionReturnExchangeLogDateTime");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
