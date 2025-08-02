using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PropertyCultureDataMap : EntityTypeConfiguration<PropertyCultureData>
    {
        public PropertyCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.PropertyID });

            // Properties
            this.Property(t => t.PropertyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PropertyName)
                .HasMaxLength(50);

            this.Property(t => t.PropertyDescription)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PropertyCultureDatas", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.PropertyID).HasColumnName("PropertyID");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.PropertyDescription).HasColumnName("PropertyDescription");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Property)
                .WithMany(t => t.PropertyCultureDatas)
                .HasForeignKey(d => d.PropertyID);
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.PropertyCultureDatas)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
