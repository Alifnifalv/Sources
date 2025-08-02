using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BlinkPoOrderDetailMap : EntityTypeConfiguration<BlinkPoOrderDetail>
    {
        public BlinkPoOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.BlinkPoOrderDetailsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BlinkPoOrderDetails");
            this.Property(t => t.BlinkPoOrderDetailsID).HasColumnName("BlinkPoOrderDetailsID");
            this.Property(t => t.RefBlinkPoOrderMasterID).HasColumnName("RefBlinkPoOrderMasterID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.QtyOrder).HasColumnName("QtyOrder");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.QtyReceived).HasColumnName("QtyReceived");

            // Relationships
            //this.HasRequired(t => t.ProductMaster)
            //    .WithMany(t => t.BlinkPoOrderDetails)
            //    .HasForeignKey(d => d.RefProductID);

        }
    }
}
