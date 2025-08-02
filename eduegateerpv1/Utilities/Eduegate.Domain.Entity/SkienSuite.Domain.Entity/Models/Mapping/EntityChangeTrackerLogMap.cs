using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityChangeTrackerLogMap : EntityTypeConfiguration<EntityChangeTrackerLog>
    {
        public EntityChangeTrackerLogMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityChangeTrackerLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("EntityChangeTrackerLog", "sync");
            this.Property(t => t.EntityChangeTrackerLogID).HasColumnName("EntityChangeTrackerLogID");
            this.Property(t => t.EntityChangeTrackerType).HasColumnName("EntityChangeTrackerType");
            this.Property(t => t.EntityChangeTrackerTypeID).HasColumnName("EntityChangeTrackerTypeID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.SyncedOn).HasColumnName("SyncedOn");
        }
    }
}
