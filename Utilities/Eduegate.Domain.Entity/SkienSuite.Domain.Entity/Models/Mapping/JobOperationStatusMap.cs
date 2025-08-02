using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobOperationStatusMap : EntityTypeConfiguration<JobOperationStatus>
    {
        public JobOperationStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.JobOperationStatusID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("JobOperationStatuses", "jobs");
            this.Property(t => t.JobOperationStatusID).HasColumnName("JobOperationStatusID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
