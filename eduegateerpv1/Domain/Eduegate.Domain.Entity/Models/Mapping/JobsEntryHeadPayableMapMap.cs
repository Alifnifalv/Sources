using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobsEntryHeadPayableMapMap : EntityTypeConfiguration<JobsEntryHeadPayableMap>
    {
        public JobsEntryHeadPayableMapMap()
        {
            // Primary Key
            this.HasKey(t => t.JobsEntryHeadPayableMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("JobsEntryHeadPayableMaps", "jobs");
            this.Property(t => t.JobsEntryHeadPayableMapIID).HasColumnName("JobsEntryHeadPayableMapIID");
            this.Property(t => t.PayableID).HasColumnName("PayableID");
            this.Property(t => t.JobEntryHeadID).HasColumnName("JobEntryHeadID");

            // Relationships
            this.HasOptional(t => t.Payable)
                .WithMany(t => t.JobsEntryHeadPayableMaps)
                .HasForeignKey(d => d.PayableID);
            this.HasOptional(t => t.JobEntryHead)
                .WithMany(t => t.JobsEntryHeadPayableMaps)
                .HasForeignKey(d => d.JobEntryHeadID);

        }
    }
}
