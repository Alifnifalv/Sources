using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobStatusMap : EntityTypeConfiguration<JobStatus>
    {
        public JobStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.JobStatusID);

            // Properties
            this.Property(t => t.JobStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("JobStatuses", "jobs");
            this.Property(t => t.JobStatusID).HasColumnName("JobStatusID");
            this.Property(t => t.JobTypeID).HasColumnName("JobTypeID");
            this.Property(t => t.SerNo).HasColumnName("SerNo");
            this.Property(t => t.StatusName).HasColumnName("StatusName");

            // Relationships
            this.HasOptional(t => t.JobActivity)
                .WithMany(t => t.JobStatuses)
                .HasForeignKey(d => d.JobTypeID);

        }
    }
}
