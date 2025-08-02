using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductInventoryConfigTempMap : EntityTypeConfiguration<ProductInventoryConfigTemp>
    {
        public ProductInventoryConfigTempMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductInventoryConfigIID);

            // Properties
            this.Property(t => t.ProductWarranty)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductInventoryConfigTemp", "catalog");
            this.Property(t => t.ProductInventoryConfigIID).HasColumnName("ProductInventoryConfigIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.NotifyQuantity).HasColumnName("NotifyQuantity");
            this.Property(t => t.MinimumQuantity).HasColumnName("MinimumQuantity");
            this.Property(t => t.MaximumQuantity).HasColumnName("MaximumQuantity");
            this.Property(t => t.MinimumQuanityInCart).HasColumnName("MinimumQuanityInCart");
            this.Property(t => t.MaximumQuantityInCart).HasColumnName("MaximumQuantityInCart");
            this.Property(t => t.IsQuntityUseDecimals).HasColumnName("IsQuntityUseDecimals");
            this.Property(t => t.BackOrderTypeID).HasColumnName("BackOrderTypeID");
            this.Property(t => t.IsStockAvailabiltiyID).HasColumnName("IsStockAvailabiltiyID");
            this.Property(t => t.ProductWarranty).HasColumnName("ProductWarranty");
            this.Property(t => t.IsSerialNumber).HasColumnName("IsSerialNumber");
            this.Property(t => t.IsSerialRequiredForPurchase).HasColumnName("IsSerialRequiredForPurchase");
            this.Property(t => t.DeliveryMethod).HasColumnName("DeliveryMethod");
            this.Property(t => t.ProductWeight).HasColumnName("ProductWeight");
            this.Property(t => t.ProductLength).HasColumnName("ProductLength");
            this.Property(t => t.ProductWidth).HasColumnName("ProductWidth");
            this.Property(t => t.ProductHeight).HasColumnName("ProductHeight");
            this.Property(t => t.DimensionalWeight).HasColumnName("DimensionalWeight");
            this.Property(t => t.PackingTypeID).HasColumnName("PackingTypeID");
            this.Property(t => t.IsMarketPlace).HasColumnName("IsMarketPlace");
            this.Property(t => t.HSCode).HasColumnName("HSCode");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.ProductCost).HasColumnName("ProductCost");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
