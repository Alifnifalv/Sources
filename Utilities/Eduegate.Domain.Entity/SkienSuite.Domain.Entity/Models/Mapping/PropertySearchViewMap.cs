using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PropertySearchViewMap : EntityTypeConfiguration<PropertySearchView>
    {
        public PropertySearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyIID);

            // Properties
            this.Property(t => t.PropertyIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PropertyName)
                .HasMaxLength(50);

            this.Property(t => t.PropertyDescription)
                .HasMaxLength(100);

            this.Property(t => t.DefaultValue)
                .HasMaxLength(50);

            this.Property(t => t.PropertyTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PropertySearchView", "catalog");
            this.Property(t => t.PropertyIID).HasColumnName("PropertyIID");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.PropertyDescription).HasColumnName("PropertyDescription");
            this.Property(t => t.DefaultValue).HasColumnName("DefaultValue");
            this.Property(t => t.PropertyTypeName).HasColumnName("PropertyTypeName");
        }
    }
}
