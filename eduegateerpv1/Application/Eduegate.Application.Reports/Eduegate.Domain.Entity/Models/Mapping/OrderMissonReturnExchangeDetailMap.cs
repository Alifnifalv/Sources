using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMissonReturnExchangeDetailMap : EntityTypeConfiguration<OrderMissonReturnExchangeDetail>
    {
        public OrderMissonReturnExchangeDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderMissonReturnExchangeDetailsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderMissonReturnExchangeDetails");
            this.Property(t => t.OrderMissonReturnExchangeDetailsID).HasColumnName("OrderMissonReturnExchangeDetailsID");
            this.Property(t => t.RefOrderMissonReturnExchangeMasterID).HasColumnName("RefOrderMissonReturnExchangeMasterID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefOrderReturnExchangeReceiptID).HasColumnName("RefOrderReturnExchangeReceiptID");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedDateTimeStamp).HasColumnName("CreatedDateTimeStamp");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.UpdatedByTimeStamp).HasColumnName("UpdatedByTimeStamp");
            this.Property(t => t.UpdatedByID).HasColumnName("UpdatedByID");
        }
    }
}
