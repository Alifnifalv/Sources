using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoOrderDetailMap : EntityTypeConfiguration<IntlPoOrderDetail>
    {
        public IntlPoOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoOrderDetailsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("IntlPoOrderDetails");
            this.Property(t => t.IntlPoOrderDetailsID).HasColumnName("IntlPoOrderDetailsID");
            this.Property(t => t.RefIntlPoOrderMasterID).HasColumnName("RefIntlPoOrderMasterID");
            this.Property(t => t.RefIntlPoRequestID).HasColumnName("RefIntlPoRequestID");
            this.Property(t => t.QtyOrder).HasColumnName("QtyOrder");
            this.Property(t => t.ItemCostUSD).HasColumnName("ItemCostUSD");
            this.Property(t => t.ItemTotalUSD).HasColumnName("ItemTotalUSD");
            this.Property(t => t.QtyCancelled).HasColumnName("QtyCancelled");
            this.Property(t => t.CancelledTotalUSD).HasColumnName("CancelledTotalUSD");
            this.Property(t => t.QtyReceived).HasColumnName("QtyReceived");

            // Relationships
            this.HasRequired(t => t.IntlPoOrderMaster)
                .WithMany(t => t.IntlPoOrderDetails)
                .HasForeignKey(d => d.RefIntlPoOrderMasterID);
            this.HasRequired(t => t.IntlPoRequest)
                .WithMany(t => t.IntlPoOrderDetails)
                .HasForeignKey(d => d.RefIntlPoRequestID);

        }
    }
}
