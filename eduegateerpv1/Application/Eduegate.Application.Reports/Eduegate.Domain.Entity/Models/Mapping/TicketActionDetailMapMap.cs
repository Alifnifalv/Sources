using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TicketActionDetailMapMap : EntityTypeConfiguration<TicketActionDetailMap>
    {
        public TicketActionDetailMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketActionDetailIID);

            // Properties
            this.Property(t => t.Reason)
                .HasMaxLength(200);

            this.Property(t => t.Remark)
                .HasMaxLength(200);

            this.Property(t => t.ReturnNumber)
                .HasMaxLength(50);

            this.Property(t => t.Timestamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("TicketActionDetailMaps", "cs");
            this.Property(t => t.TicketActionDetailIID).HasColumnName("TicketActionDetailIID");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.RefundTypeID).HasColumnName("RefundTypeID");
            this.Property(t => t.RefundAmount).HasColumnName("RefundAmount");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.ReturnNumber).HasColumnName("ReturnNumber");
            this.Property(t => t.GiveItemTo).HasColumnName("GiveItemTo");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.IssueType).HasColumnName("IssueType");
            this.Property(t => t.AssignedEmployee).HasColumnName("AssignedEmployee");
            this.Property(t => t.Timestamps).HasColumnName("Timestamps");

            // Relationships
            this.HasRequired(t => t.Ticket)
                .WithMany(t => t.TicketActionDetailMaps)
                .HasForeignKey(d => d.TicketID);

        }
    }
}
