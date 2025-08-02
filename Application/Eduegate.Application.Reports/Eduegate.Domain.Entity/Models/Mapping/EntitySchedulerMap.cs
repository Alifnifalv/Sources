using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntitySchedulerMap : EntityTypeConfiguration<EntityScheduler>
    {
        public EntitySchedulerMap()
        {
            // Primary Key
            this.HasKey(t => t.EntitySchedulerIID);

            // Properties
            this.Property(t => t.EntityValue)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EntitySchedulers", "schedulers");
            this.Property(t => t.EntitySchedulerIID).HasColumnName("EntitySchedulerIID");
            this.Property(t => t.SchedulerTypeID).HasColumnName("SchedulerTypeID");
            this.Property(t => t.SchedulerEntityTypeID).HasColumnName("SchedulerEntityTypeID");
            this.Property(t => t.EntityID).HasColumnName("EntityID");
            this.Property(t => t.EntityValue).HasColumnName("EntityValue");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.SchedulerEntityType)
                .WithMany(t => t.EntitySchedulers)
                .HasForeignKey(d => d.SchedulerEntityTypeID);
            this.HasOptional(t => t.SchedulerType)
                .WithMany(t => t.EntitySchedulers)
                .HasForeignKey(d => d.SchedulerTypeID);

        }
    }
}
