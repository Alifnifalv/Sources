using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobTypeMap : EntityTypeConfiguration<JobType>
    {
        public JobTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.JobTypeID);

            // Properties
            this.Property(t => t.JobTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.JobTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("JobTypes", "payroll");
            this.Property(t => t.JobTypeID).HasColumnName("JobTypeID");
            this.Property(t => t.JobTypeName).HasColumnName("JobTypeName");
        }
    }
}
