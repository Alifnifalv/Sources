using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUMapMap : EntityTypeConfiguration<ProductSKUMap>
    {
        public ProductSKUMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSKUMapIID);

            // Properties
            this.Property(t => t.ProductSKUCode)
                .HasMaxLength(100);

            this.Property(t => t.PartNo)
                .HasMaxLength(50);

            this.Property(t => t.BarCode)
                .HasMaxLength(50);

            this.Property(t => t.VariantsMap)
                .HasMaxLength(200);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            this.Property(t => t.SKUName)
                .HasMaxLength(1000);


            // Table & Column Mappings
            this.ToTable("ProductSKUMaps", "catalog");
            this.Property(t => t.ProductSKUMapIID).HasColumnName("ProductSKUMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
            this.Property(t => t.ProductSKUCode).HasColumnName("ProductSKUCode");
            this.Property(t => t.PartNo).HasColumnName("PartNo");
            this.Property(t => t.BarCode).HasColumnName("BarCode");
            this.Property(t => t.ProductPrice).HasColumnName("ProductPrice");
            this.Property(t => t.VariantsMap).HasColumnName("VariantsMap");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.IsHiddenFromList).HasColumnName("IsHiddenFromList");
            this.Property(t => t.HideSKU).HasColumnName("HideSKU");
            this.Property(t => t.SKUName).HasColumnName("SKUName");
            this.Property(t => t.SeoMetadataID).HasColumnName("SeoMetadataID"); 

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductSKUMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductStatu)
                .WithMany(t => t.ProductSKUMaps)
                .HasForeignKey(d => d.StatusID);
            this.HasOptional(t => t.SeoMetadata)
               .WithMany(t => t.ProductSKUMaps)
               .HasForeignKey(d => d.SeoMetadataID);
        }
    }
}
