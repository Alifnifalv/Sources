using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceProviderMap : EntityTypeConfiguration<ServiceProvider>
    {
        public ServiceProviderMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceProviderID);

            // Properties
            this.Property(t => t.ServiceProviderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProviderCode)
                .HasMaxLength(20);

            this.Property(t => t.ProviderName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.ServiceProviderLink)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ServiceProviders", "distribution");
            this.Property(t => t.ServiceProviderID).HasColumnName("ServiceProviderID");
            this.Property(t => t.ProviderCode).HasColumnName("ProviderCode");
            this.Property(t => t.ProviderName).HasColumnName("ProviderName");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.ServiceProviderLink).HasColumnName("ServiceProviderLink");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.ServiceProviders)
                .HasForeignKey(d => d.CountryID);

        }
    }
}
