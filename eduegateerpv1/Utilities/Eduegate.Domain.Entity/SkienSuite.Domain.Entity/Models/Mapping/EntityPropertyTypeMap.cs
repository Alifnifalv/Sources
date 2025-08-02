using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityPropertyTypeMap : EntityTypeConfiguration<EntityPropertyType>
    {
        public EntityPropertyTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityPropertyTypeID);

            // Properties
            this.Property(t => t.EntityPropertyTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EntityPropertyTypeName)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("EntityPropertyTypes", "mutual");
            this.Property(t => t.EntityPropertyTypeID).HasColumnName("EntityPropertyTypeID");
            this.Property(t => t.EntityPropertyTypeName).HasColumnName("EntityPropertyTypeName");
        }
    }
}
