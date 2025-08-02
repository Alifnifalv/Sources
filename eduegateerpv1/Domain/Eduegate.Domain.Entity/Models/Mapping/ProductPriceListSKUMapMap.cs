using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListSKUMapMap : EntityTypeConfiguration<ProductPriceListSKUMap>
    {
        public ProductPriceListSKUMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListItemMapIID);

            // Properties
            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListSKUMaps", "catalog");
            this.Property(t => t.ProductPriceListItemMapIID).HasColumnName("ProductPriceListItemMapIID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.ProductSKUID).HasColumnName("ProductSKUID");
            this.Property(t => t.UnitGroundID).HasColumnName("UnitGroundID");
            this.Property(t => t.CustomerGroupID).HasColumnName("CustomerGroupID");
            this.Property(t => t.SellingQuantityLimit).HasColumnName("SellingQuantityLimit");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.PricePercentage).HasColumnName("PricePercentage");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.Cost).HasColumnName("Cost");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.ProductPriceList)
                .WithMany(t => t.ProductPriceListSKUMaps)
                .HasForeignKey(d => d.ProductPriceListID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductPriceListSKUMaps)
                .HasForeignKey(d => d.ProductSKUID);
            this.HasOptional(t => t.UnitGroup)
                .WithMany(t => t.ProductPriceListSKUMaps)
                .HasForeignKey(d => d.UnitGroundID);
            this.HasOptional(t => t.CustomerGroup)
                .WithMany(t => t.ProductPriceListSKUMaps)
                .HasForeignKey(d => d.CustomerGroupID);

        }
    }
}
