using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SKUPaymentMethodExceptionMapMap : EntityTypeConfiguration<SKUPaymentMethodExceptionMap>
    {
        public SKUPaymentMethodExceptionMapMap()
        {
            // Primary Key
            this.HasKey(t => t.SKUPaymentMethodExceptionMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SKUPaymentMethodExceptionMaps", "inventory");
            this.Property(t => t.SKUPaymentMethodExceptionMapIID).HasColumnName("SKUPaymentMethodExceptionMapIID");
            this.Property(t => t.SKUID).HasColumnName("SKUID");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.AreaID).HasColumnName("AreaID");

            // Relationships
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.SKUPaymentMethodExceptionMaps)
                .HasForeignKey(d => d.SKUID);
            this.HasOptional(t => t.PaymentMethod)
                .WithMany(t => t.SKUPaymentMethodExceptionMaps)
                .HasForeignKey(d => d.PaymentMethodID);

        }
    }
}
