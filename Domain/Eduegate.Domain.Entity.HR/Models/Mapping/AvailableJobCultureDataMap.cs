using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.HR.Models.Mapping
{
    public class AvailableJobCultureDataMap : EntityTypeConfiguration<AvailableJobCultureData>
    {
        public AvailableJobCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.JobIID });

            // Properties
            this.Property(t => t.JobTitle)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.JobDescription)
             .IsRequired()
             .HasMaxLength(1000);

            this.Property(t => t.JobDetails)
              .IsRequired()
              .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("AvailableJobCultureDatas", "hr");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.JobIID).HasColumnName("JobIID");
            this.Property(t => t.JobTitle).HasColumnName("JobTitle");
            this.Property(t => t.JobDescription).HasColumnName("JobDescription");
            this.Property(t => t.JobDetails).HasColumnName("JobDetails");
        }
    }
}
