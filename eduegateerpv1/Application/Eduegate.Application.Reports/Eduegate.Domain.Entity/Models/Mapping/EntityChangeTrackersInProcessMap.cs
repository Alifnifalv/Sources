using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityChangeTrackersInProcessMap : EntityTypeConfiguration<EntityChangeTrackersInProcess>
    {
        public EntityChangeTrackersInProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityChangeTrackerInProcessIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EntityChangeTrackersInProcess", "sync");
            this.Property(t => t.EntityChangeTrackerInProcessIID).HasColumnName("EntityChangeTrackerInProcessIID");
            this.Property(t => t.EntityChangeTrackerID).HasColumnName("EntityChangeTrackerID");
            this.Property(t => t.IsReprocess).HasColumnName("IsReprocess");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.EntityChangeTracker)
                .WithMany(t => t.EntityChangeTrackersInProcesses)
                .HasForeignKey(d => d.EntityChangeTrackerID);

        }
    }
}
