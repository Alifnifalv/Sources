using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServicePricingMap : EntityTypeConfiguration<ServicePricing>
    {
        public ServicePricingMap()
        {
            // Primary Key
            this.HasKey(t => t.ServicePricingIID);

            // Properties
            this.Property(t => t.Caption)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ServicePricings", "saloon");
            this.Property(t => t.ServicePricingIID).HasColumnName("ServicePricingIID");
            this.Property(t => t.ServiceID).HasColumnName("ServiceID");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.DiscountPrice).HasColumnName("DiscountPrice");
            this.Property(t => t.Caption).HasColumnName("Caption");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Service)
                .WithMany(t => t.ServicePricings)
                .HasForeignKey(d => d.ServiceID);

        }
    }
}
