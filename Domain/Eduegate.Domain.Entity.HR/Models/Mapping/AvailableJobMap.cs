using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.HR.Models.Mapping
{
    public class AvailableJobMap : EntityTypeConfiguration<AvailableJob>
    {
        public AvailableJobMap()
        {
            // Primary Key
            this.HasKey(t => t.JobIID);

            // Properties
            this.Property(t => t.JobTitle)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.TypeOfJob)
                .IsRequired();

            this.Property(t => t.JobDescription)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("AvailableJobs", "hr");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.TypeOfJob).HasColumnName("TypeOfJob");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.JobDescription).HasColumnName("JobDescription");
            this.Property(t => t.JobDetails).HasColumnName("JobDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PageId).HasColumnName("PageId");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.JobIID).HasColumnName("JobIID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
