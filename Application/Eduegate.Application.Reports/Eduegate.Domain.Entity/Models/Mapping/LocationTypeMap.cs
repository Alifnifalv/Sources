using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LocationTypeMap : EntityTypeConfiguration<LocationType>
    {
        public LocationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.LocationTypeID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LocationTypes", "inventory");
            this.Property(t => t.LocationTypeID).HasColumnName("LocationTypeID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
