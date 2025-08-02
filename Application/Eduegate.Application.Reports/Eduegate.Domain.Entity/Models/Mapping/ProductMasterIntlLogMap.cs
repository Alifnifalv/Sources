using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductMasterIntlLogMap : EntityTypeConfiguration<ProductMasterIntlLog>
    {
        public ProductMasterIntlLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductMasterIntlLogID);

            // Properties
            this.Property(t => t.ProductType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ProductType_N)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ProductMasterIntlLog");
            this.Property(t => t.ProductMasterIntlLogID).HasColumnName("ProductMasterIntlLogID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.ProductType).HasColumnName("ProductType");
            this.Property(t => t.ProductType_N).HasColumnName("ProductType_N");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductPrice_N).HasColumnName("ProductPrice_N");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.ProductDiscountPrice_N).HasColumnName("ProductDiscountPrice_N");
            this.Property(t => t.ProductActive).HasColumnName("ProductActive");
            this.Property(t => t.ProductActive_N).HasColumnName("ProductActive_N");
            this.Property(t => t.ProductActiveCountry).HasColumnName("ProductActiveCountry");
            this.Property(t => t.ProductActiveCountry_N).HasColumnName("ProductActiveCountry_N");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.DeliveryDays_N).HasColumnName("DeliveryDays_N");
            this.Property(t => t.MultiPrice).HasColumnName("MultiPrice");
            this.Property(t => t.MultiPrice_N).HasColumnName("MultiPrice_N");
            this.Property(t => t.MultiPriceIgnore).HasColumnName("MultiPriceIgnore");
            this.Property(t => t.MultiPriceIgnore_N).HasColumnName("MultiPriceIgnore_N");
        }
    }
}
