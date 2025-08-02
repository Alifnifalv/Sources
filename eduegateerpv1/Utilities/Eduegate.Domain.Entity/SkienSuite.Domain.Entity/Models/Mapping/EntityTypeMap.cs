using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityTypeMap : EntityTypeConfiguration<EntityType>
    {
        public EntityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityTypeID);

            // Properties
            this.Property(t => t.EntityTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EntityName)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("EntityTypes", "mutual");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");
            this.Property(t => t.EntityName).HasColumnName("EntityName");
        }
    }
}
