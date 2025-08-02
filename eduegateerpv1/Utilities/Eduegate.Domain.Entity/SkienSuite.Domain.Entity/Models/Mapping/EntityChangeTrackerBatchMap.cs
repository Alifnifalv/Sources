using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityChangeTrackerBatchMap : EntityTypeConfiguration<EntityChangeTrackerBatch>
    {
        public EntityChangeTrackerBatchMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityChangeTrackerBatchID);

            // Properties
            this.Property(t => t.EntityChangeTrackerBatchNo)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("EntityChangeTrackerBatch", "sync");
            this.Property(t => t.EntityChangeTrackerBatchID).HasColumnName("EntityChangeTrackerBatchID");
            this.Property(t => t.EntityChangeTrackerBatchNo).HasColumnName("EntityChangeTrackerBatchNo");
            this.Property(t => t.NoOfProducts).HasColumnName("NoOfProducts");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
