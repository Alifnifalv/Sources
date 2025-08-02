using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderReturnExchangeMap : EntityTypeConfiguration<OrderReturnExchange>
    {
        public OrderReturnExchangeMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderReturnExchangeRequestID);

            // Properties
            this.Property(t => t.Status)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("OrderReturnExchange");
            this.Property(t => t.OrderReturnExchangeRequestID).HasColumnName("OrderReturnExchangeRequestID");
            this.Property(t => t.OrderReturnExchangeReceiptID).HasColumnName("OrderReturnExchangeReceiptID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ReturnExchange).HasColumnName("ReturnExchange");
            this.Property(t => t.IsMission).HasColumnName("IsMission");
            this.Property(t => t.UpdatedQty).HasColumnName("UpdatedQty");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDateTimeStamp).HasColumnName("CreatedDateTimeStamp");
        }
    }
}
