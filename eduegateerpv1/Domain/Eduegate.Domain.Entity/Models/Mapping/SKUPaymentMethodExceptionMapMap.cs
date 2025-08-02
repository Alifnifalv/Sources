using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SKUPaymentMethodExceptionMapMap : EntityTypeConfiguration<SKUPaymentMethodExceptionMaps>
    {
        public SKUPaymentMethodExceptionMapMap()  
        { 
            // Primary Key
            this.HasKey(t => new { t.SKUPaymentMethodExceptionMapIID });
             
            // Properties
            this.Property(t => t.SKUPaymentMethodExceptionMapIID)
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("SKUPaymentMethodExceptionMaps", "inventory");
            this.Property(t => t.SKUID).HasColumnName("SKUID");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CompnayID).HasColumnName("CompnayID");
            this.Property(t => t.AreaID).HasColumnName("AreaID");

            // Relationships
            //this.HasRequired(t => t.ProductSKUMaps)
            //    .WithMany(t => t.SKUPaymentMethodExceptionMaps)
            //    .HasForeignKey(d => d.SKUID);
            this.HasRequired(t => t.PaymentMethod)
                .WithMany(t => t.SKUPaymentMethodExceptionMaps)
                .HasForeignKey(d => d.PaymentMethodID);
        }
    }
}
