using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobMasterMap : EntityTypeConfiguration<JobMaster>
    {
        public JobMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.JobID);

            // Properties
            this.Property(t => t.JobName)
                .IsRequired()
                .HasMaxLength(70);

            this.Property(t => t.JobDescription)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("JobMaster");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.JobName).HasColumnName("JobName");
            this.Property(t => t.JobDescription).HasColumnName("JobDescription");
            this.Property(t => t.JobPosition).HasColumnName("JobPosition");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
