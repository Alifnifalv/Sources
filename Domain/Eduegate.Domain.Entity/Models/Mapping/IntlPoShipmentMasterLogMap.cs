using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoShipmentMasterLogMap : EntityTypeConfiguration<IntlPoShipmentMasterLog>
    {
        public IntlPoShipmentMasterLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoShipmentMasterLogID);

            // Properties
            this.Property(t => t.ShipmentStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IntlPoShipmentMasterLog");
            this.Property(t => t.IntlPoShipmentMasterLogID).HasColumnName("IntlPoShipmentMasterLogID");
            this.Property(t => t.RefIntlPoShipmentMasterID).HasColumnName("RefIntlPoShipmentMasterID");
            this.Property(t => t.ShipmentStatus).HasColumnName("ShipmentStatus");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");

            // Relationships
            this.HasRequired(t => t.IntlPoShipmentMaster)
                .WithMany(t => t.IntlPoShipmentMasterLogs)
                .HasForeignKey(d => d.RefIntlPoShipmentMasterID);

        }
    }
}
