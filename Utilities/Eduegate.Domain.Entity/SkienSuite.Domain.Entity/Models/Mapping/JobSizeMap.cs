using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobSizeMap : EntityTypeConfiguration<JobSize>
    {
        public JobSizeMap()
        {
            // Primary Key
            this.HasKey(t => t.JobSizeID);

            // Properties
            this.Property(t => t.JobSizeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("JobSizes", "jobs");
            this.Property(t => t.JobSizeID).HasColumnName("JobSizeID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
