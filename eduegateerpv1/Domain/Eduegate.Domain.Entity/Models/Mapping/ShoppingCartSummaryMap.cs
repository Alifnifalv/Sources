using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ShoppingCartSummaryMap : EntityTypeConfiguration<ShoppingCartSummary>
    {
        public ShoppingCartSummaryMap()
        {
            // Primary Key
            this.HasKey(t => t.ShoppingCartSummaryID);

            // Properties
            this.Property(t => t.CustomerSessionID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VoucherNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ShoppingCartSummary");
            this.Property(t => t.ShoppingCartSummaryID).HasColumnName("ShoppingCartSummaryID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.OrderTotal).HasColumnName("OrderTotal");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.InitStatus).HasColumnName("InitStatus");
            this.Property(t => t.InitDateTime).HasColumnName("InitDateTime");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.GrandTotalApp).HasColumnName("GrandTotalApp");
            this.Property(t => t.VoucherNo).HasColumnName("VoucherNo");
            this.Property(t => t.VoucherAmount).HasColumnName("VoucherAmount");
            this.Property(t => t.CartTotal).HasColumnName("CartTotal");
            this.Property(t => t.DeliveryMethod).HasColumnName("DeliveryMethod");
            this.Property(t => t.DeliveryCharges).HasColumnName("DeliveryCharges");
            this.Property(t => t.ActualDeliveryCharges).HasColumnName("ActualDeliveryCharges");
            this.Property(t => t.DeliveryDiscount).HasColumnName("DeliveryDiscount");
            this.Property(t => t.ExpressDeliveryCharges).HasColumnName("ExpressDeliveryCharges");
            this.Property(t => t.ExpressDeliveryDiscount).HasColumnName("ExpressDeliveryDiscount");
            this.Property(t => t.NextDayDeliveryCharges).HasColumnName("NextDayDeliveryCharges");
            this.Property(t => t.NextDayDeliveryDiscount).HasColumnName("NextDayDeliveryDiscount");
        }
    }
}
