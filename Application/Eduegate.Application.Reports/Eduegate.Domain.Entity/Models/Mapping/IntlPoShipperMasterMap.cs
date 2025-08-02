using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoShipperMasterMap : EntityTypeConfiguration<IntlPoShipperMaster>
    {
        public IntlPoShipperMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoShipperMasterID);

            // Properties
            this.Property(t => t.IntlPoShipperName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("IntlPoShipperMaster");
            this.Property(t => t.IntlPoShipperMasterID).HasColumnName("IntlPoShipperMasterID");
            this.Property(t => t.IntlPoShipperName).HasColumnName("IntlPoShipperName");
            this.Property(t => t.IntlPoShipperActive).HasColumnName("IntlPoShipperActive");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
