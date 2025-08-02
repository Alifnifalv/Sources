using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDeliveryCountrySettingMap : EntityTypeConfiguration<ProductDeliveryCountrySetting>
    {
        public ProductDeliveryCountrySettingMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductDeliveryCountrySettingsIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductDeliveryCountrySettings", "inventory");
            this.Property(t => t.ProductDeliveryCountrySettingsIID).HasColumnName("ProductDeliveryCountrySettingsIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductDeliveryCountrySettings)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductDeliveryCountrySettings)
                .HasForeignKey(d => d.ProductSKUMapID);

        }
    }
}
