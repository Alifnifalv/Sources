using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductInventoryMap : EntityTypeConfiguration<vwProductInventory>
    {
        public vwProductInventoryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductID, t.ProductPartNo, t.ProductName, t.BrandNameEn, t.ProductAvailableQuantity, t.ProductDiscountPrice, t.ProductActive, t.ProductReOrderLevel, t.SupplierID, t.ProductManagerID });

            // Properties
            this.Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductBarCode)
                .HasMaxLength(100);

            this.Property(t => t.ProductPartNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductGroup)
                .HasMaxLength(100);

            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.BrandNameEn)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProductAvailableQuantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductDiscountPrice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductCategoryAll)
                .HasMaxLength(100);

            this.Property(t => t.ProductReOrderLevel)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SupplierID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductManagerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwProductInventory");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductBarCode).HasColumnName("ProductBarCode");
            this.Property(t => t.ProductPartNo).HasColumnName("ProductPartNo");
            this.Property(t => t.ProductCreatedOn).HasColumnName("ProductCreatedOn");
            this.Property(t => t.ProductGroup).HasColumnName("ProductGroup");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.BrandNameEn).HasColumnName("BrandNameEn");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.ProductAvailableQuantity).HasColumnName("ProductAvailableQuantity");
            this.Property(t => t.ordercnt).HasColumnName("ordercnt");
            this.Property(t => t.itemsold).HasColumnName("itemsold");
            this.Property(t => t.ProductDiscountPrice).HasColumnName("ProductDiscountPrice");
            this.Property(t => t.ProductActive).HasColumnName("ProductActive");
            this.Property(t => t.ProductWeight).HasColumnName("ProductWeight");
            this.Property(t => t.ProductCategoryAll).HasColumnName("ProductCategoryAll");
            this.Property(t => t.ProductReOrderLevel).HasColumnName("ProductReOrderLevel");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.ProductManagerID).HasColumnName("ProductManagerID");
        }
    }
}
