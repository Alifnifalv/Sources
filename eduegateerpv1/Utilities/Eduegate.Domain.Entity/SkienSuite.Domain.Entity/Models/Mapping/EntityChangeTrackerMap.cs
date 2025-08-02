using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityChangeTrackerMap : EntityTypeConfiguration<EntityChangeTracker>
    {
        public EntityChangeTrackerMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityChangeTrackerIID);

            // Properties
            this.Property(t => t.ProcessedFields)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.BatchNo)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("EntityChangeTracker", "sync");
            this.Property(t => t.EntityChangeTrackerIID).HasColumnName("EntityChangeTrackerIID");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.OperationTypeID).HasColumnName("OperationTypeID");
            this.Property(t => t.ProcessedID).HasColumnName("ProcessedID");
            this.Property(t => t.ProcessedFields).HasColumnName("ProcessedFields");
            this.Property(t => t.TrackerStatusID).HasColumnName("TrackerStatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");

            // Relationships
            this.HasOptional(t => t.SyncEntity)
                .WithMany(t => t.EntityChangeTrackers)
                .HasForeignKey(d => d.EntityID);
            this.HasOptional(t => t.OperationType)
                .WithMany(t => t.EntityChangeTrackers)
                .HasForeignKey(d => d.OperationTypeID);
            this.HasOptional(t => t.TrackerStatus)
                .WithMany(t => t.EntityChangeTrackers)
                .HasForeignKey(d => d.TrackerStatusID);

        }
    }
}
