using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoShipmentBoxDetailMap : EntityTypeConfiguration<IntlPoShipmentBoxDetail>
    {
        public IntlPoShipmentBoxDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoShipmentBoxDetailsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("IntlPoShipmentBoxDetails");
            this.Property(t => t.IntlPoShipmentBoxDetailsID).HasColumnName("IntlPoShipmentBoxDetailsID");
            this.Property(t => t.RefIntlPoShipmentMasterID).HasColumnName("RefIntlPoShipmentMasterID");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.IntlPoShipmentMaster)
                .WithMany(t => t.IntlPoShipmentBoxDetails)
                .HasForeignKey(d => d.RefIntlPoShipmentMasterID);

        }
    }
}
