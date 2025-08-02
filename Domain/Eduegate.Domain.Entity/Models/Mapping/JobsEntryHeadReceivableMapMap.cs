using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobsEntryHeadReceivableMapMap : EntityTypeConfiguration<JobsEntryHeadReceivableMap>
    {
        public JobsEntryHeadReceivableMapMap()
        {
            // Primary Key
            this.HasKey(t => t.JobsEntryHeadReceivableMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("JobsEntryHeadReceivableMaps", "jobs");
            this.Property(t => t.JobsEntryHeadReceivableMapIID).HasColumnName("JobsEntryHeadReceivableMapIID");
            this.Property(t => t.ReceivableID).HasColumnName("ReceivableID");
            this.Property(t => t.JobEntryHeadID).HasColumnName("JobEntryHeadID");

            // Relationships
            this.HasOptional(t => t.Receivable)
                .WithMany(t => t.JobsEntryHeadReceivableMaps)
                .HasForeignKey(d => d.ReceivableID);
            this.HasOptional(t => t.JobEntryHead)
                .WithMany(t => t.JobsEntryHeadReceivableMaps)
                .HasForeignKey(d => d.JobEntryHeadID);

        }
    }
}
