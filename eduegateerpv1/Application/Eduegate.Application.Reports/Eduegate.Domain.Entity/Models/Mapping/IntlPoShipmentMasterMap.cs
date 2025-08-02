using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoShipmentMasterMap : EntityTypeConfiguration<IntlPoShipmentMaster>
    {
        public IntlPoShipmentMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoShipmentMasterID);

            // Properties
            this.Property(t => t.AwbNo)
                .HasMaxLength(20);

            this.Property(t => t.ShipmentOrgin)
                .HasMaxLength(30);

            this.Property(t => t.ShipmentDestination)
                .HasMaxLength(30);

            this.Property(t => t.ShipmentStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("IntlPoShipmentMaster");
            this.Property(t => t.IntlPoShipmentMasterID).HasColumnName("IntlPoShipmentMasterID");
            this.Property(t => t.RefIntlPoShipperMasterID).HasColumnName("RefIntlPoShipperMasterID");
            this.Property(t => t.ShipmentDate).HasColumnName("ShipmentDate");
            this.Property(t => t.NoOfBoxes).HasColumnName("NoOfBoxes");
            this.Property(t => t.ShipmentCostTotalUSD).HasColumnName("ShipmentCostTotalUSD");
            this.Property(t => t.CustomsCostTotalUSD).HasColumnName("CustomsCostTotalUSD");
            this.Property(t => t.OtherCostTotalUSD).HasColumnName("OtherCostTotalUSD");
            this.Property(t => t.AwbNo).HasColumnName("AwbNo");
            this.Property(t => t.ShipmentOrgin).HasColumnName("ShipmentOrgin");
            this.Property(t => t.ShipmentDestination).HasColumnName("ShipmentDestination");
            this.Property(t => t.ExpectedDateOfArrival).HasColumnName("ExpectedDateOfArrival");
            this.Property(t => t.ShipmentStatus).HasColumnName("ShipmentStatus");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasRequired(t => t.IntlPoShipperMaster)
                .WithMany(t => t.IntlPoShipmentMasters)
                .HasForeignKey(d => d.RefIntlPoShipperMasterID);

        }
    }
}
