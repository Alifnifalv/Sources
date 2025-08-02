using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityPropertyMap : EntityTypeConfiguration<EntityProperty>
    {
        public EntityPropertyMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityPropertyIID);

            // Properties
            this.Property(t => t.PropertyName)
                .HasMaxLength(50);

            this.Property(t => t.PropertyDescription)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EntityProperties", "mutual");
            this.Property(t => t.EntityPropertyIID).HasColumnName("EntityPropertyIID");
            this.Property(t => t.EntityPropertyTypeID).HasColumnName("EntityPropertyTypeID");
            this.Property(t => t.PropertyName).HasColumnName("PropertyName");
            this.Property(t => t.PropertyDescription).HasColumnName("PropertyDescription");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.EntityPropertyType)
                .WithMany(t => t.EntityProperties)
                .HasForeignKey(d => d.EntityPropertyTypeID);

        }
    }
}
