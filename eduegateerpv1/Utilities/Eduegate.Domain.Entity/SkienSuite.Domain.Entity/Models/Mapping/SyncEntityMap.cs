using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SyncEntityMap : EntityTypeConfiguration<SyncEntity>
    {
        public SyncEntityMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityID);

            // Properties
            this.Property(t => t.EntityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EntityName)
                .HasMaxLength(50);

            this.Property(t => t.EntityDataSource)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("SyncEntities", "sync");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.EntityName).HasColumnName("EntityName");
            this.Property(t => t.EntityDataSource).HasColumnName("EntityDataSource");
        }
    }
}
