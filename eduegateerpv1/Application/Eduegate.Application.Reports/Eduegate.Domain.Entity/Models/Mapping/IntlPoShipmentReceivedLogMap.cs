using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoShipmentReceivedLogMap : EntityTypeConfiguration<IntlPoShipmentReceivedLog>
    {
        public IntlPoShipmentReceivedLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoShipmentReceivedLogID);

            // Properties
            this.Property(t => t.ShipmentReceivedStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IntlPoShipmentReceivedLog");
            this.Property(t => t.IntlPoShipmentReceivedLogID).HasColumnName("IntlPoShipmentReceivedLogID");
            this.Property(t => t.RefIntlPoShipmentReceivedID).HasColumnName("RefIntlPoShipmentReceivedID");
            this.Property(t => t.ShipmentReceivedStatus).HasColumnName("ShipmentReceivedStatus");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");

            // Relationships
            this.HasRequired(t => t.IntlPoShipmentReceived)
                .WithMany(t => t.IntlPoShipmentReceivedLogs)
                .HasForeignKey(d => d.RefIntlPoShipmentReceivedID);

        }
    }
}
