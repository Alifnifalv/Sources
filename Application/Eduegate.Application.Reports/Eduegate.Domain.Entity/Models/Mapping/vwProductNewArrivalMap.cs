using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductNewArrivalMap : EntityTypeConfiguration<vwProductNewArrival>
    {
        public vwProductNewArrivalMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductName, t.BrandNameEn, t.ProductPrice, t.ProductDiscountPrice, t.prodname, t.RefNewArrivalProductID, t.ProductStartDate, t.ProductEndDate, t.BrandCode, t.BrandID, t.BrandPosition, t.ProductAvailableQuantity, t.NewArrival, t.ProductNameBrand, t.ProductActive, t.ProductActiveAr, t.MultiPrice });

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

            this.Property(t => t.prodname)
                .IsRequired()
                .HasMaxLength(123);

            this.Property(t => t.RefNewArrivalProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductThumbnail)
                .HasMaxLength(50);

            this.Property(t => t.ProductCategoryAll)
                .HasMaxLength(100);

            this.Property(t => t.BrandCode)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.BrandID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BrandPosition)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductAvailableQuantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NewArrival)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductNameBrand)
                .IsRequired()
                .HasMaxLength(123);

            this.Property(t => t.ProductNameAr)
                .HasMaxLength(555);

            this.Property(t => t.prodnameAr)
                .HasMaxLength(813);

            this.Property(t => t.ProductNameBrandAr)
                .HasMaxLength(813);

            this.Property(t => t.BrandNameAr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("vwProductNewArrivals");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.BrandNameEn).HasColumnName("BrandNameEn");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.prodname).HasColumnName("prodname");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.RefNewArrivalProductID).HasColumnName("RefNewArrivalProductID");
            this.Property(t => t.ProductThumbnail).HasColumnName("ProductThumbnail");
            this.Property(t => t.ProductStartDate).HasColumnName("ProductStartDate");
            this.Property(t => t.ProductEndDate).HasColumnName("ProductEndDate");
            this.Property(t => t.ProductCategoryAll).HasColumnName("ProductCategoryAll");
            this.Property(t => t.BrandCode).HasColumnName("BrandCode");
            this.Property(t => t.ProductCreatedOn).HasColumnName("ProductCreatedOn");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.BrandPosition).HasColumnName("BrandPosition");
            this.Property(t => t.ProductAvailableQuantity).HasColumnName("ProductAvailableQuantity");
            this.Property(t => t.NewArrival).HasColumnName("NewArrival");
            this.Property(t => t.ProductNameBrand).HasColumnName("ProductNameBrand");
            this.Property(t => t.QuantityDiscount).HasColumnName("QuantityDiscount");
            this.Property(t => t.ProductNameAr).HasColumnName("ProductNameAr");
            this.Property(t => t.prodnameAr).HasColumnName("prodnameAr");
            this.Property(t => t.ProductNameBrandAr).HasColumnName("ProductNameBrandAr");
            this.Property(t => t.BrandNameAr).HasColumnName("BrandNameAr");
            this.Property(t => t.ProductActive).HasColumnName("ProductActive");
            this.Property(t => t.ProductActiveAr).HasColumnName("ProductActiveAr");
            this.Property(t => t.MultiPrice).HasColumnName("MultiPrice");
        }
    }
}
