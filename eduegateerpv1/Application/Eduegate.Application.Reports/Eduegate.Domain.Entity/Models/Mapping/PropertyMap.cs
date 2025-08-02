using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PropertyMap : EntityTypeConfiguration<Property>
    {
        public PropertyMap()
        {
            // Primary Key
            this.HasKey(t => t.PropertyIID);

            // Properties
            this.Property(t => t.PropertyName)
                .HasMaxLength(50);

            this.Property(t => t.PropertyDescription)
                .HasMaxLength(100);

            this.Property(t => t.DefaultValue)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Properties", "catalog");
            this.Property(t => t.PropertyIID).HasColumnName("PropertyIID");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.PropertyDescription).HasColumnName("PropertyDescription");
            this.Property(t => t.DefaultValue).HasColumnName("DefaultValue");
            this.Property(t => t.PropertyTypeID).HasColumnName("PropertyTypeID");
            this.Property(t => t.IsUnqiue).HasColumnName("IsUnqiue");
            this.Property(t => t.UIControlTypeID).HasColumnName("UIControlTypeID");
            this.Property(t => t.UIControlValidationID).HasColumnName("UIControlValidationID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.UIControlType)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.UIControlTypeID);
            this.HasOptional(t => t.UIControlValidation)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.UIControlValidationID);
            this.HasOptional(t => t.PropertyType)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.PropertyTypeID);

        }
    }
}
