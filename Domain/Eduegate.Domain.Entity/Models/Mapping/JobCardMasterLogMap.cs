using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobCardMasterLogMap : EntityTypeConfiguration<JobCardMasterLog>
    {
        public JobCardMasterLogMap()
        {
            // Primary Key
            this.HasKey(t => t.JobCardMasterLogID);

            // Properties
            this.Property(t => t.JobCardStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("JobCardMasterLog");
            this.Property(t => t.JobCardMasterLogID).HasColumnName("JobCardMasterLogID");
            this.Property(t => t.RefJobCardMasterID).HasColumnName("RefJobCardMasterID");
            this.Property(t => t.JobCardStatus).HasColumnName("JobCardStatus");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.JobCardMaster)
                .WithMany(t => t.JobCardMasterLogs)
                .HasForeignKey(d => d.RefJobCardMasterID);

        }
    }
}
