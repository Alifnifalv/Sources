using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductMasterIntlMap : EntityTypeConfiguration<ProductMasterIntl>
    {
        public ProductMasterIntlMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductMasterIntlID);

            // Properties
            this.Property(t => t.ProductType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ProductMasterIntl");
            this.Property(t => t.ProductMasterIntlID).HasColumnName("ProductMasterIntlID");
            this.Property(t => t.RefProductMasterID).HasColumnName("RefProductMasterID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.ProductType).HasColumnName("ProductType");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.ProductAvailableQty).HasColumnName("ProductAvailableQty");
            this.Property(t => t.ProductActive).HasColumnName("ProductActive");
            this.Property(t => t.ProductActiveCountry).HasColumnName("ProductActiveCountry");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ProductDiscountPercent).HasColumnName("ProductDiscountPercent");
            this.Property(t => t.MultiPrice).HasColumnName("MultiPrice");
            this.Property(t => t.MultiPriceIgnore).HasColumnName("MultiPriceIgnore");
            this.Property(t => t.IsProductVoucher).HasColumnName("IsProductVoucher");
            this.Property(t => t.QuantityDiscount).HasColumnName("QuantityDiscount");
            this.Property(t => t.IntlShipping).HasColumnName("IntlShipping");
            this.Property(t => t.NextDayDeliveryCost).HasColumnName("NextDayDeliveryCost");
            this.Property(t => t.ProductCostPrice).HasColumnName("ProductCostPrice");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.RefSupplierID).HasColumnName("RefSupplierID");
            this.Property(t => t.KwtInventory).HasColumnName("KwtInventory");
        }
    }
}
