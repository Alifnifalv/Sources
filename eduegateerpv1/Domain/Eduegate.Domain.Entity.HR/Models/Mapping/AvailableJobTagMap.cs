using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.HR.Models.Mapping
{
    public class AvailableJobTagMap : EntityTypeConfiguration<AvailableJobTag>
    {
        public AvailableJobTagMap()
        {
            // Primary Key
            this.HasKey(t => t.AvailableJobTagIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AvailableJobTags", "hr");
            this.Property(t => t.AvailableJobTagIID).HasColumnName("AvailableJobTagIID");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.AvailableJobs)
                .WithMany(t => t.AvailableJobTags)
                .HasForeignKey(d => d.JobID);
        }
    }
}
