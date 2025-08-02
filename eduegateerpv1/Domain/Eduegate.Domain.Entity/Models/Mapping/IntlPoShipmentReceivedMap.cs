using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoShipmentReceivedMap : EntityTypeConfiguration<IntlPoShipmentReceived>
    {
        public IntlPoShipmentReceivedMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoShipmentReceivedID);

            // Properties
            this.Property(t => t.ProductPartNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ShipmentReceivedStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("IntlPoShipmentReceived");
            this.Property(t => t.IntlPoShipmentReceivedID).HasColumnName("IntlPoShipmentReceivedID");
            this.Property(t => t.RefIntlPoShipmentDetailsID).HasColumnName("RefIntlPoShipmentDetailsID");
            this.Property(t => t.ReceivedDate).HasColumnName("ReceivedDate");
            this.Property(t => t.ProductPartNo).HasColumnName("ProductPartNo");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.QtyReceived).HasColumnName("QtyReceived");
            this.Property(t => t.UnitCostUSD).HasColumnName("UnitCostUSD");
            this.Property(t => t.ShipmentReceivedStatus).HasColumnName("ShipmentReceivedStatus");
            this.Property(t => t.InvoiceDone).HasColumnName("InvoiceDone");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasRequired(t => t.IntlPoShipmentDetail)
                .WithMany(t => t.IntlPoShipmentReceiveds)
                .HasForeignKey(d => d.RefIntlPoShipmentDetailsID);

        }
    }
}
