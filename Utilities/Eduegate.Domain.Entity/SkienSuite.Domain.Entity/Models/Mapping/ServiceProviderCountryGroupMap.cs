using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceProviderCountryGroupMap : EntityTypeConfiguration<ServiceProviderCountryGroup>
    {
        public ServiceProviderCountryGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryGroupID);

            // Properties
            this.Property(t => t.CountryGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("ServiceProviderCountryGroups", "distribution");
            this.Property(t => t.CountryGroupID).HasColumnName("CountryGroupID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ServiceProviderID).HasColumnName("ServiceProviderID");

            // Relationships
            this.HasOptional(t => t.ServiceProvider)
                .WithMany(t => t.ServiceProviderCountryGroups)
                .HasForeignKey(d => d.ServiceProviderID);

        }
    }
}
