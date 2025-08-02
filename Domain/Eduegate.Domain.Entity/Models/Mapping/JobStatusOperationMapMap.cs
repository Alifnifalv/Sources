using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobStatusOperationMapMap : EntityTypeConfiguration<JobStatusOperationMap>
    {
        public JobStatusOperationMapMap()
        {
            // Primary Key
            this.HasKey(t => t.JobStatusOperationMapId);

            // Properties
            this.Property(t => t.JobStatusOperationMapId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            // Table & Column Mappings
            this.ToTable("JobStatusOperationMaps", "jobs");
            this.Property(t => t.JobStatusOperationMapId).HasColumnName("JobStatusOperationMapId");
            this.Property(t => t.JobStatusId).HasColumnName("JobStatusId");
            this.Property(t => t.JobOperationStatusId).HasColumnName("JobOperationStatusId");
            this.Property(t => t.JobTypeId).HasColumnName("JobTypeId");
        }
    }
}
