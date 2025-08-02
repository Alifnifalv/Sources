using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityChangeTrackersQueueMap : EntityTypeConfiguration<EntityChangeTrackersQueue>
    {
        public EntityChangeTrackersQueueMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityChangeTrackerQueueIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("EntityChangeTrackersQueue", "sync");
            this.Property(t => t.EntityChangeTrackerQueueIID).HasColumnName("EntityChangeTrackerQueueIID");
            this.Property(t => t.EntityChangeTrackeID).HasColumnName("EntityChangeTrackeID");
            this.Property(t => t.IsReprocess).HasColumnName("IsReprocess");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            this.HasOptional(t => t.EntityChangeTracker)
                .WithMany(t => t.EntityChangeTrackersQueues)
                .HasForeignKey(d => d.EntityChangeTrackeID);

        }
    }
}
