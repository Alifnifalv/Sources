using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductUsedProductMap : EntityTypeConfiguration<vwProductUsedProduct>
    {
        public vwProductUsedProductMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductName, t.BrandNameEn, t.Position, t.ProductPrice, t.ProductDiscountPrice, t.prodname, t.BrandCode, t.ProductAvailableQuantity, t.ProductActive, t.ProductActiveAr });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductCode)
                .HasMaxLength(150);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.BrandNameEn)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductDiscountPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductThumbnail)
                .HasMaxLength(81);

            this.Property(t => t.prodname)
                .IsRequired()
                .HasMaxLength(123);

            this.Property(t => t.ProductCategoryAll)
                .HasMaxLength(100);

            this.Property(t => t.BrandCode)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.ProductAvailableQuantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwProductUsedProducts");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.BrandNameEn).HasColumnName("BrandNameEn");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.ProductThumbnail).HasColumnName("ProductThumbnail");
            this.Property(t => t.prodname).HasColumnName("prodname");
            this.Property(t => t.ProductCategoryAll).HasColumnName("ProductCategoryAll");
            this.Property(t => t.BrandCode).HasColumnName("BrandCode");
            this.Property(t => t.QuantityDiscount).HasColumnName("QuantityDiscount");
            this.Property(t => t.ProductAvailableQuantity).HasColumnName("ProductAvailableQuantity");
            this.Property(t => t.ProductActive).HasColumnName("ProductActive");
            this.Property(t => t.ProductActiveAr).HasColumnName("ProductActiveAr");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
        }
    }
}
