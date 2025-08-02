using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListCustomerGroupMapMap : EntityTypeConfiguration<ProductPriceListCustomerGroupMap>
    {
        public ProductPriceListCustomerGroupMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListCustomerGroupMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListCustomerGroupMaps", "catalog");
            this.Property(t => t.ProductPriceListCustomerGroupMapIID).HasColumnName("ProductPriceListCustomerGroupMapIID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.CustomerGroupID).HasColumnName("CustomerGroupID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.DiscountPrice).HasColumnName("DiscountPrice");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.Price).HasColumnName("Price");

            // Relationships
            this.HasOptional(t => t.Brand)
                .WithMany(t => t.ProductPriceListCustomerGroupMaps)
                .HasForeignKey(d => d.BrandID);
            this.HasOptional(t => t.Category)
                .WithMany(t => t.ProductPriceListCustomerGroupMaps)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.CustomerGroup)
                .WithMany(t => t.ProductPriceListCustomerGroupMaps)
                .HasForeignKey(d => d.CustomerGroupID);
            this.HasOptional(t => t.ProductPriceList)
                .WithMany(t => t.ProductPriceListCustomerGroupMaps)
                .HasForeignKey(d => d.ProductPriceListID);
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductPriceListCustomerGroupMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductPriceListCustomerGroupMaps)
                .HasForeignKey(d => d.ProductSKUMapID);

        }
    }
}
