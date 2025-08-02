using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PropertyTypePropertyMapMap : EntityTypeConfiguration<PropertyTypePropertyMap>
    {
        public PropertyTypePropertyMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PropertyTypeID, t.PropertyID });

            // Properties
            this.Property(t => t.PropertyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PropertyTypePropertyMaps", "catalog");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");

            // Relationships
            this.HasRequired(t => t.PropertyType)
                .WithMany(t => t.PropertyTypePropertyMaps)
                .HasForeignKey(d => d.PropertyTypeID);

        }
    }
}
