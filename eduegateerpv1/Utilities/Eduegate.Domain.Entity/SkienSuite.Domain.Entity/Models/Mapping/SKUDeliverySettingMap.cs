using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SKUDeliverySettingMap : EntityTypeConfiguration<SKUDeliverySetting>
    {
        public SKUDeliverySettingMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSKUMapIID);

            // Properties
            this.Property(t => t.ProductSKUCode)
                .HasMaxLength(150);

            this.Property(t => t.SKUName)
                .HasMaxLength(1000);

            this.Property(t => t.PartNo)
                .HasMaxLength(50);

            this.Property(t => t.BarCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SKUDeliverySettings", "inventory");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.ProductSKUCode).HasColumnName("ProductSKUCode");
            this.Property(t => t.SKUName).HasColumnName("SKUName");
            this.Property(t => t.PartNo).HasColumnName("PartNo");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
        }
    }
}
