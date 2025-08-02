using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityTypeRelationMapMap : EntityTypeConfiguration<EntityTypeRelationMap>
    {
        public EntityTypeRelationMapMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityTypeRelationMapsIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EntityTypeRelationMaps", "mutual");
            this.Property(t => t.EntityTypeRelationMapsIID).HasColumnName("EntityTypeRelationMapsIID");
            this.Property(t => t.FromEntityTypeID).HasColumnName("FromEntityTypeID");
            this.Property(t => t.ToEntityTypeID).HasColumnName("ToEntityTypeID");
            this.Property(t => t.FromRelationID).HasColumnName("FromRelationID");
            this.Property(t => t.ToRelationID).HasColumnName("ToRelationID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.EntityType)
                .WithMany(t => t.EntityTypeRelationMaps)
                .HasForeignKey(d => d.FromEntityTypeID);
            this.HasOptional(t => t.EntityType1)
                .WithMany(t => t.EntityTypeRelationMaps1)
                .HasForeignKey(d => d.ToEntityTypeID);

        }
    }
}
