using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SchedulerTypeMap : EntityTypeConfiguration<SchedulerType>
    {
        public SchedulerTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.SchedulerTypeID);

            // Properties
            this.Property(t => t.SchedulerTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SchedulerTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SchedulerTypes", "schedulers");
            this.Property(t => t.SchedulerTypeID).HasColumnName("SchedulerTypeID");
            this.Property(t => t.SchedulerTypeName).HasColumnName("SchedulerTypeName");
        }
    }
}
