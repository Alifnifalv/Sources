using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoShipmentPaymentMasterMap : EntityTypeConfiguration<IntlPoShipmentPaymentMaster>
    {
        public IntlPoShipmentPaymentMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoShipmentPaymentMasterID);

            // Properties
            this.Property(t => t.TransType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PaymentText)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IntlPoShipmentPaymentMaster");
            this.Property(t => t.IntlPoShipmentPaymentMasterID).HasColumnName("IntlPoShipmentPaymentMasterID");
            this.Property(t => t.RefIntlPoShipmentMasterID).HasColumnName("RefIntlPoShipmentMasterID");
            this.Property(t => t.IntlPoShipmentPaymentTypeID).HasColumnName("IntlPoShipmentPaymentTypeID");
            this.Property(t => t.TransType).HasColumnName("TransType");
            this.Property(t => t.PaymentText).HasColumnName("PaymentText");
            this.Property(t => t.RefIntlPoBankAccountID).HasColumnName("RefIntlPoBankAccountID");
            this.Property(t => t.AmountPaidUSD).HasColumnName("AmountPaidUSD");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");

            // Relationships
            this.HasRequired(t => t.IntlPoBankAccount)
                .WithMany(t => t.IntlPoShipmentPaymentMasters)
                .HasForeignKey(d => d.RefIntlPoBankAccountID);
            this.HasRequired(t => t.IntlPoShipmentMaster)
                .WithMany(t => t.IntlPoShipmentPaymentMasters)
                .HasForeignKey(d => d.RefIntlPoShipmentMasterID);

        }
    }
}
