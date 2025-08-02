using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoOrderMasterPaymentAdditionalMap : EntityTypeConfiguration<IntlPoOrderMasterPaymentAdditional>
    {
        public IntlPoOrderMasterPaymentAdditionalMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoOrderMasterPaymentAdditionalID);

            // Properties
            this.Property(t => t.PaymentType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PaymentText)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IntlPoOrderMasterPaymentAdditional");
            this.Property(t => t.IntlPoOrderMasterPaymentAdditionalID).HasColumnName("IntlPoOrderMasterPaymentAdditionalID");
            this.Property(t => t.RefIntlPoOrderMasterID).HasColumnName("RefIntlPoOrderMasterID");
            this.Property(t => t.PaymentType).HasColumnName("PaymentType");
            this.Property(t => t.PaymentText).HasColumnName("PaymentText");
            this.Property(t => t.PaymentAmountUSD).HasColumnName("PaymentAmountUSD");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.PaymentStatus).HasColumnName("PaymentStatus");

            // Relationships
            this.HasRequired(t => t.IntlPoOrderMaster)
                .WithMany(t => t.IntlPoOrderMasterPaymentAdditionals)
                .HasForeignKey(d => d.RefIntlPoOrderMasterID);

        }
    }
}
