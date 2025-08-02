using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderPaymentMap : EntityTypeConfiguration<OrderPayment>
    {
        public OrderPaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.PaymentID);

            // Properties
            this.Property(t => t.TransID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OrderPayment");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.RefPaymentOrderID).HasColumnName("RefPaymentOrderID");
            this.Property(t => t.RefPaymentOrderItemID).HasColumnName("RefPaymentOrderItemID");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
            this.Property(t => t.PaymentAmount).HasColumnName("PaymentAmount");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.TransStatus).HasColumnName("TransStatus");
            this.Property(t => t.TransDate).HasColumnName("TransDate");

            // Relationships
            this.HasRequired(t => t.OrderMaster)
                .WithMany(t => t.OrderPayments)
                .HasForeignKey(d => d.RefPaymentOrderID);

        }
    }
}
