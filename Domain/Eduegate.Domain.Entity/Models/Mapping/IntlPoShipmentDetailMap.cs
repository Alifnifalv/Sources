using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoShipmentDetailMap : EntityTypeConfiguration<IntlPoShipmentDetail>
    {
        public IntlPoShipmentDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoShipmentDetailsID);

            // Properties
            this.Property(t => t.MadeIn)
                .HasMaxLength(100);

            this.Property(t => t.HSCode)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("IntlPoShipmentDetails");
            this.Property(t => t.IntlPoShipmentDetailsID).HasColumnName("IntlPoShipmentDetailsID");
            this.Property(t => t.RefIntlPoGrnDetailsID).HasColumnName("RefIntlPoGrnDetailsID");
            this.Property(t => t.RefIntlPoShipmentBoxDetailsID).HasColumnName("RefIntlPoShipmentBoxDetailsID");
            this.Property(t => t.UnitCostUSD).HasColumnName("UnitCostUSD");
            this.Property(t => t.QtyShipped).HasColumnName("QtyShipped");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.MadeIn).HasColumnName("MadeIn");
            this.Property(t => t.Weight).HasColumnName("Weight");
            this.Property(t => t.HSCode).HasColumnName("HSCode");
            this.Property(t => t.QtyReceived).HasColumnName("QtyReceived");

            // Relationships
            this.HasRequired(t => t.IntlPoGrnDetail)
                .WithMany(t => t.IntlPoShipmentDetails)
                .HasForeignKey(d => d.RefIntlPoGrnDetailsID);
            this.HasRequired(t => t.IntlPoShipmentBoxDetail)
                .WithMany(t => t.IntlPoShipmentDetails)
                .HasForeignKey(d => d.RefIntlPoShipmentBoxDetailsID);

        }
    }
}
