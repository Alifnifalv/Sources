using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderItemID);

            // Properties
            this.Property(t => t.CancelledBy)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DigitalDelivered)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DeliveryRemarks)
                .HasMaxLength(150);

            this.Property(t => t.DeliveryRemarksAr)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("OrderItem");
            this.Property(t => t.OrderItemID).HasColumnName("OrderItemID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefOrderProductID).HasColumnName("RefOrderProductID");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.ItemAmount).HasColumnName("ItemAmount");
            this.Property(t => t.ItemCanceled).HasColumnName("ItemCanceled");
            this.Property(t => t.CancelledBy).HasColumnName("CancelledBy");
            this.Property(t => t.RefOrderStatusID).HasColumnName("RefOrderStatusID");
            this.Property(t => t.ReturnApproveQty).HasColumnName("ReturnApproveQty");
            this.Property(t => t.ProductMultiPrice).HasColumnName("ProductMultiPrice");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.ExpressDelivery).HasColumnName("ExpressDelivery");
            this.Property(t => t.PickupShowroom).HasColumnName("PickupShowroom");
            this.Property(t => t.SendbyEmail).HasColumnName("SendbyEmail");
            this.Property(t => t.IsNextDay).HasColumnName("IsNextDay");
            this.Property(t => t.IsDigital).HasColumnName("IsDigital");
            this.Property(t => t.DigitalDelivered).HasColumnName("DigitalDelivered");
            this.Property(t => t.FreeGift).HasColumnName("FreeGift");
            this.Property(t => t.DeliveryRemarks).HasColumnName("DeliveryRemarks");
            this.Property(t => t.DeliveryRemarksAr).HasColumnName("DeliveryRemarksAr");

            // Relationships
            this.HasRequired(t => t.OrderMaster)
                .WithMany(t => t.OrderItems)
                .HasForeignKey(d => d.RefOrderID);
            //this.HasRequired(t => t.ProductMaster)
            //    .WithMany(t => t.OrderItems)
            //    .HasForeignKey(d => d.RefOrderProductID);
            //this.HasRequired(t => t.ProductMaster1)
            //    .WithMany(t => t.OrderItems1)
            //    .HasForeignKey(d => d.RefOrderProductID);

        }
    }
}
