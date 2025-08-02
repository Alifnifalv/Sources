using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PropertyTypeSearchViewMap : EntityTypeConfiguration<PropertyTypeSearchView>
    {
        public PropertyTypeSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyTypeID);

            // Properties
            this.Property(t => t.PropertyTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PropertyTypeSearchView", "catalog");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.PropertyTypeName).HasColumnName("PropertyTypeName");
        }
    }
}
