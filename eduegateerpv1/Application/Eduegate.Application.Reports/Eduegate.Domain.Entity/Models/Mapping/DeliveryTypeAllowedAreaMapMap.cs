using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypeAllowedAreaMapMap : EntityTypeConfiguration<DeliveryTypeAllowedAreaMap>
    {
        public DeliveryTypeAllowedAreaMapMap()
        {
            // Primary Key
            this.HasKey(t => t.AreaDeliveryTypeMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DeliveryTypeAllowedAreaMaps", "inventory");
            this.Property(t => t.AreaDeliveryTypeMapIID).HasColumnName("AreaDeliveryTypeMapIID");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.AreaID).HasColumnName("AreaID");
            this.Property(t => t.CartTotalFrom).HasColumnName("CartTotalFrom");
            this.Property(t => t.CartTotalTo).HasColumnName("CartTotalTo");
            this.Property(t => t.DeliveryCharge).HasColumnName("DeliveryCharge");
            this.Property(t => t.DeliveryChargePercentage).HasColumnName("DeliveryChargePercentage");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.IsSelected).HasColumnName("IsSelected");

            // Relationships
            this.HasOptional(t => t.Area)
                .WithMany(t => t.DeliveryTypeAllowedAreaMaps)
                .HasForeignKey(d => d.AreaID);
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.DeliveryTypeAllowedAreaMaps)
                .HasForeignKey(d => d.DeliveryTypeID);

        }
    }
}
