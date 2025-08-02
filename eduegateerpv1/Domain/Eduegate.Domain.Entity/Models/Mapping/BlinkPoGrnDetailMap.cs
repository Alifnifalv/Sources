using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BlinkPoGrnDetailMap : EntityTypeConfiguration<BlinkPoGrnDetail>
    {
        public BlinkPoGrnDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.BlinkPoGrnDetailsID);

            // Properties
            this.Property(t => t.BlinkPoGrnDetailsBin)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("BlinkPoGrnDetails");
            this.Property(t => t.BlinkPoGrnDetailsID).HasColumnName("BlinkPoGrnDetailsID");
            this.Property(t => t.RefBlinkPoGrnMasterID).HasColumnName("RefBlinkPoGrnMasterID");
            this.Property(t => t.RefBlinkPoOrderDetailsID).HasColumnName("RefBlinkPoOrderDetailsID");
            this.Property(t => t.QtyReceived).HasColumnName("QtyReceived");
            this.Property(t => t.QtyShipped).HasColumnName("QtyShipped");
            this.Property(t => t.BlinkPoGrnDetailsBin).HasColumnName("BlinkPoGrnDetailsBin");

            // Relationships
            this.HasRequired(t => t.BlinkPoGrnMaster)
                .WithMany(t => t.BlinkPoGrnDetails)
                .HasForeignKey(d => d.RefBlinkPoGrnMasterID);
            this.HasRequired(t => t.BlinkPoOrderDetail)
                .WithMany(t => t.BlinkPoGrnDetails)
                .HasForeignKey(d => d.RefBlinkPoOrderDetailsID);

        }
    }
}
