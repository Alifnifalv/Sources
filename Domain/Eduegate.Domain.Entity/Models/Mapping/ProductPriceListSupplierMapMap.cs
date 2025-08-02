using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPriceListSupplierMapMap : EntityTypeConfiguration<ProductPriceListSupplierMap>
    {
        public ProductPriceListSupplierMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPriceListSupplierMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductPriceListSupplierMaps", "catalog");
            this.Property(t => t.ProductPriceListSupplierMapIID).HasColumnName("ProductPriceListSupplierMapIID");
            this.Property(t => t.ProductPriceListID).HasColumnName("ProductPriceListID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            //this.HasOptional(t => t.ProductPriceList)
            //    .WithMany(t => t.ProductPriceListSupplierMaps)
            //    .HasForeignKey(d => d.ProductPriceListID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.ProductPriceListSupplierMaps)
                .HasForeignKey(d => d.SupplierID);

        }
    }
}
