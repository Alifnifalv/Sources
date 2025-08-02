using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PropertyTypeCultureDataMap : EntityTypeConfiguration<PropertyTypeCultureData>
    {
        public PropertyTypeCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.PropertyTypeID });

            // Properties
            this.Property(t => t.PropertyTypeName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PropertyTypeCultureDatas", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.PropertyTypeName).HasColumnName("PropertyTypeName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
