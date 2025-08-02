using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoOrderMasterPaymentMap : EntityTypeConfiguration<IntlPoOrderMasterPayment>
    {
        public IntlPoOrderMasterPaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoOrderMasterPaymentID);

            // Properties
            this.Property(t => t.TransType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IntlPoOrderMasterPayment");
            this.Property(t => t.IntlPoOrderMasterPaymentID).HasColumnName("IntlPoOrderMasterPaymentID");
            this.Property(t => t.RefIntlPoOrderMasterID).HasColumnName("RefIntlPoOrderMasterID");
            this.Property(t => t.RefIntlPoBankAccountID).HasColumnName("RefIntlPoBankAccountID");
            this.Property(t => t.AmountPaidUSD).HasColumnName("AmountPaidUSD");
            this.Property(t => t.TransType).HasColumnName("TransType");
            this.Property(t => t.TransActive).HasColumnName("TransActive");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");

            // Relationships
            this.HasRequired(t => t.IntlPoBankAccount)
                .WithMany(t => t.IntlPoOrderMasterPayments)
                .HasForeignKey(d => d.RefIntlPoBankAccountID);
            this.HasRequired(t => t.IntlPoOrderMaster)
                .WithMany(t => t.IntlPoOrderMasterPayments)
                .HasForeignKey(d => d.RefIntlPoOrderMasterID);

        }
    }
}
