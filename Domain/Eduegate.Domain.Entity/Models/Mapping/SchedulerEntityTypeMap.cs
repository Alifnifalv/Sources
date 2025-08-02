using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SchedulerEntityTypeMap : EntityTypeConfiguration<SchedulerEntityType>
    {
        public SchedulerEntityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.SchedulerEntityTypID);

            // Properties
            this.Property(t => t.SchedulerEntityTypID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EntityName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SchedulerEntityTypes", "schedulers");
            this.Property(t => t.SchedulerEntityTypID).HasColumnName("SchedulerEntityTypID");
            this.Property(t => t.EntityName).HasColumnName("EntityName");
        }
    }
}
