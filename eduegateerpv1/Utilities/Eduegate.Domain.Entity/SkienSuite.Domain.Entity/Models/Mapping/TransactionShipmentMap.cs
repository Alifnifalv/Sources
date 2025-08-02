using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionShipmentMap : EntityTypeConfiguration<TransactionShipment>
    {
        public TransactionShipmentMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionShipmentIID);

            // Properties
            this.Property(t => t.ShipmentReference)
                .HasMaxLength(50);

            this.Property(t => t.FreightCarrier)
                .HasMaxLength(50);

            this.Property(t => t.AirWayBillNo)
                .HasMaxLength(50);

            this.Property(t => t.BrokerAccount)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("TransactionShipments", "inventory");
            this.Property(t => t.TransactionShipmentIID).HasColumnName("TransactionShipmentIID");
            this.Property(t => t.TransactionHeadID).HasColumnName("TransactionHeadID");
            this.Property(t => t.SupplierIDFrom).HasColumnName("SupplierIDFrom");
            this.Property(t => t.SupplierIDTo).HasColumnName("SupplierIDTo");
            this.Property(t => t.ShipmentReference).HasColumnName("ShipmentReference");
            this.Property(t => t.FreightCarrier).HasColumnName("FreightCarrier");
            this.Property(t => t.ClearanceTypeID).HasColumnName("ClearanceTypeID");
            this.Property(t => t.AirWayBillNo).HasColumnName("AirWayBillNo");
            this.Property(t => t.FreightCharges).HasColumnName("FreightCharges");
            this.Property(t => t.BrokerCharges).HasColumnName("BrokerCharges");
            this.Property(t => t.AdditionalCharges).HasColumnName("AdditionalCharges");
            this.Property(t => t.Weight).HasColumnName("Weight");
            this.Property(t => t.NoOfBoxes).HasColumnName("NoOfBoxes");
            this.Property(t => t.BrokerAccount).HasColumnName("BrokerAccount");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.TransactionShipments)
                .HasForeignKey(d => d.TransactionHeadID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.TransactionShipments)
                .HasForeignKey(d => d.SupplierIDFrom);
            this.HasOptional(t => t.Supplier1)
                .WithMany(t => t.TransactionShipments1)
                .HasForeignKey(d => d.SupplierIDTo);

        }
    }
}
