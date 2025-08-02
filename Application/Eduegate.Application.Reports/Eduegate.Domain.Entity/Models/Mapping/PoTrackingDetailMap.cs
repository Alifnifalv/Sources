using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PoTrackingDetailMap : EntityTypeConfiguration<PoTrackingDetail>
    {
        public PoTrackingDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.PoTrackingDetailsID);

            // Properties
            this.Property(t => t.Remarks)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("PoTrackingDetails");
            this.Property(t => t.PoTrackingDetailsID).HasColumnName("PoTrackingDetailsID");
            this.Property(t => t.RefPoTrackingMasterID).HasColumnName("RefPoTrackingMasterID");
            this.Property(t => t.RefPoTrackingWorkflowID).HasColumnName("RefPoTrackingWorkflowID");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
        }
    }
}
