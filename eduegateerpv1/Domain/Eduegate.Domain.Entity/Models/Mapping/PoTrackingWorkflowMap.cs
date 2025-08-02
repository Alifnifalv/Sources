using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PoTrackingWorkflowMap : EntityTypeConfiguration<PoTrackingWorkflow>
    {
        public PoTrackingWorkflowMap()
        {
            // Primary Key
            this.HasKey(t => t.PoTrackingWorkflowID);

            // Properties
            this.Property(t => t.ProcessName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PoTrackingWorkflow");
            this.Property(t => t.PoTrackingWorkflowID).HasColumnName("PoTrackingWorkflowID");
            this.Property(t => t.PoTrackingWorkflowNo).HasColumnName("PoTrackingWorkflowNo");
            this.Property(t => t.ProcessName).HasColumnName("ProcessName");
            this.Property(t => t.ProcessSeq).HasColumnName("ProcessSeq");
        }
    }
}
