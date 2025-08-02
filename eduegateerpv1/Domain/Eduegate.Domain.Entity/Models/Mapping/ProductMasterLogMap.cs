using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductMasterLogMap : EntityTypeConfiguration<ProductMasterLog>
    {
        public ProductMasterLogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.UpdateAction)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.ProductBarCode)
                .HasMaxLength(100);

            this.Property(t => t.ProductBarCode_N)
                .HasMaxLength(100);

            this.Property(t => t.Location)
                .HasMaxLength(50);

            this.Property(t => t.Location_N)
                .HasMaxLength(50);

            this.Property(t => t.ProductCategoryAll)
                .HasMaxLength(100);

            this.Property(t => t.ProductCategoryAll_N)
                .HasMaxLength(100);

            this.Property(t => t.ProductHSCode)
                .HasMaxLength(30);

            this.Property(t => t.ProductHSCode_N)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("ProductMasterLog");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductPrice_N).HasColumnName("ProductPrice_N");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.ProductDiscountPrice_N).HasColumnName("ProductDiscountPrice_N");
            this.Property(t => t.ProductAvailableQuantity).HasColumnName("ProductAvailableQuantity");
            this.Property(t => t.ProductAvailableQuantity_N).HasColumnName("ProductAvailableQuantity_N");
            this.Property(t => t.ProductActive).HasColumnName("ProductActive");
            this.Property(t => t.ProductActive_N).HasColumnName("ProductActive_N");
            this.Property(t => t.MaxOrderQty).HasColumnName("MaxOrderQty");
            this.Property(t => t.MaxOrderQty_N).HasColumnName("MaxOrderQty_N");
            this.Property(t => t.MaxCustomerQty).HasColumnName("MaxCustomerQty");
            this.Property(t => t.MaxCustomerQty_N).HasColumnName("MaxCustomerQty_N");
            this.Property(t => t.MaxCustomerQtyDuration).HasColumnName("MaxCustomerQtyDuration");
            this.Property(t => t.MaxCustomerQtyDuration_N).HasColumnName("MaxCustomerQtyDuration_N");
            this.Property(t => t.MaxOrderQtyVerified).HasColumnName("MaxOrderQtyVerified");
            this.Property(t => t.MaxOrderQtyVerified_N).HasColumnName("MaxOrderQtyVerified_N");
            this.Property(t => t.UpdateAction).HasColumnName("UpdateAction");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.ProductBarCode).HasColumnName("ProductBarCode");
            this.Property(t => t.ProductBarCode_N).HasColumnName("ProductBarCode_N");
            this.Property(t => t.Location).HasColumnName("Location");
            this.Property(t => t.Location_N).HasColumnName("Location_N");
            this.Property(t => t.ProductCategoryAll).HasColumnName("ProductCategoryAll");
            this.Property(t => t.ProductCategoryAll_N).HasColumnName("ProductCategoryAll_N");
            this.Property(t => t.ProductCostPrice).HasColumnName("ProductCostPrice");
            this.Property(t => t.ProductCostPrice_N).HasColumnName("ProductCostPrice_N");
            this.Property(t => t.CashBack).HasColumnName("CashBack");
            this.Property(t => t.CashBack_N).HasColumnName("CashBack_N");
            this.Property(t => t.ProductHSCode).HasColumnName("ProductHSCode");
            this.Property(t => t.ProductHSCode_N).HasColumnName("ProductHSCode_N");
        }
    }
}
