using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobActivityMap : EntityTypeConfiguration<JobActivity>
    {
        public JobActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.JobActivityID);

            // Properties
            this.Property(t => t.JobActivityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActivityName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("JobActivities", "jobs");
            this.Property(t => t.JobActivityID).HasColumnName("JobActivityID");
            this.Property(t => t.ActivityName).HasColumnName("ActivityName");
        }
    }
}
