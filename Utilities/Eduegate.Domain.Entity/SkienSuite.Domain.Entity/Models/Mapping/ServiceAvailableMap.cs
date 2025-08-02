using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceAvailableMap : EntityTypeConfiguration<ServiceAvailable>
    {
        public ServiceAvailableMap()
        {
            // Primary Key
            this.HasKey(t => t.ServiceAvailableID);

            // Properties
            this.Property(t => t.ServiceAvailableID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ServiceAvailables", "saloon");
            this.Property(t => t.ServiceAvailableID).HasColumnName("ServiceAvailableID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
