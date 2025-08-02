using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDayDealLogMap : EntityTypeConfiguration<ProductDayDealLog>
    {
        public ProductDayDealLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductDayDealLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductDayDealLog");
            this.Property(t => t.ProductDayDealLogID).HasColumnName("ProductDayDealLogID");
            this.Property(t => t.RefDealID).HasColumnName("RefDealID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.ProductAvailableQtyLog).HasColumnName("ProductAvailableQtyLog");
            this.Property(t => t.PickUpShowroomLog).HasColumnName("PickUpShowroomLog");
            this.Property(t => t.MaxCustomerQtyLog).HasColumnName("MaxCustomerQtyLog");
            this.Property(t => t.MaxOrderQtyLog).HasColumnName("MaxOrderQtyLog");
            this.Property(t => t.MaxCustomerQtyDurationLog).HasColumnName("MaxCustomerQtyDurationLog");
            this.Property(t => t.MaxOrderQtyVerifiedLog).HasColumnName("MaxOrderQtyVerifiedLog");
            this.Property(t => t.MultiPrice).HasColumnName("MultiPrice");
            this.Property(t => t.QuantityDiscount).HasColumnName("QuantityDiscount");
            this.Property(t => t.RefProductDiscountPrice).HasColumnName("RefProductDiscountPrice");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
