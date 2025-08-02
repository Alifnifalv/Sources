using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoOrderMasterMap : EntityTypeConfiguration<IntlPoOrderMaster>
    {
        public IntlPoOrderMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoOrderMasterID);

            // Properties
            this.Property(t => t.ReferenceDetails)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.OtherDetails)
                .HasMaxLength(300);

            this.Property(t => t.IntlPoOrderStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IntlPoOrderMaster");
            this.Property(t => t.IntlPoOrderMasterID).HasColumnName("IntlPoOrderMasterID");
            this.Property(t => t.RefIntlPoVendorID).HasColumnName("RefIntlPoVendorID");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.CreatedForPM).HasColumnName("CreatedForPM");
            this.Property(t => t.ItemTotalUSD).HasColumnName("ItemTotalUSD");
            this.Property(t => t.AdditionalCostTotalUSD).HasColumnName("AdditionalCostTotalUSD");
            this.Property(t => t.DiscountCostTotalUSD).HasColumnName("DiscountCostTotalUSD");
            this.Property(t => t.CancelledTotalUSD).HasColumnName("CancelledTotalUSD");
            this.Property(t => t.OrderTotalUSD).HasColumnName("OrderTotalUSD");
            this.Property(t => t.ReferenceDetails).HasColumnName("ReferenceDetails");
            this.Property(t => t.OtherDetails).HasColumnName("OtherDetails");
            this.Property(t => t.IntlPoOrderStatus).HasColumnName("IntlPoOrderStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.PaymentTotal).HasColumnName("PaymentTotal");
        }
    }
}
