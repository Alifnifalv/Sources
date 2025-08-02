using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerGroupDeliveryTypeMapMap : EntityTypeConfiguration<CustomerGroupDeliveryTypeMap>
    {
        public CustomerGroupDeliveryTypeMapMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerGroupDeliveryTypeMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CustomerGroupDeliveryTypeMaps", "inventory");
            this.Property(t => t.CustomerGroupDeliveryTypeMapIID).HasColumnName("CustomerGroupDeliveryTypeMapIID");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.CustomerGroupID).HasColumnName("CustomerGroupID");
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
            this.HasOptional(t => t.CustomerGroup)
                .WithMany(t => t.CustomerGroupDeliveryTypeMaps)
                .HasForeignKey(d => d.CustomerGroupID);
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.CustomerGroupDeliveryTypeMaps)
                .HasForeignKey(d => d.DeliveryTypeID);

        }
    }
}
