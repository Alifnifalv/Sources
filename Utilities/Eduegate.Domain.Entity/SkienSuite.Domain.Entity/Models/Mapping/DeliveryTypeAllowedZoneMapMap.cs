using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypeAllowedZoneMapMap : EntityTypeConfiguration<DeliveryTypeAllowedZoneMap>
    {
        public DeliveryTypeAllowedZoneMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ZoneDeliveryTypeMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DeliveryTypeAllowedZoneMaps", "inventory");
            this.Property(t => t.ZoneDeliveryTypeMapIID).HasColumnName("ZoneDeliveryTypeMapIID");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.ZoneID).HasColumnName("ZoneID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.DeliveryCharge).HasColumnName("DeliveryCharge");
            this.Property(t => t.DeliveryChargePercentage).HasColumnName("DeliveryChargePercentage");
            this.Property(t => t.CartTotalFrom).HasColumnName("CartTotalFrom");
            this.Property(t => t.CartTotalTo).HasColumnName("CartTotalTo");
            this.Property(t => t.IsSelected).HasColumnName("IsSelected");

            // Relationships
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.DeliveryTypeAllowedZoneMaps)
                .HasForeignKey(d => d.DeliveryTypeID);
            this.HasOptional(t => t.Zone)
                .WithMany(t => t.DeliveryTypeAllowedZoneMaps)
                .HasForeignKey(d => d.ZoneID);

        }
    }
}
