using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

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
                .HasMaxLength(150);

            this.Property(t => t.PartNo)
                .HasMaxLength(50);

            this.Property(t => t.BarCode)
                .HasMaxLength(50);

            this.Property(t => t.VariantsMap)
                .HasMaxLength(200);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.SKUName)
                .HasMaxLength(1000);

            this.Property(t => t.ProductSKUMapIIDTEXT)
                .HasMaxLength(20);

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
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.IsHiddenFromList).HasColumnName("IsHiddenFromList");
            this.Property(t => t.HideSKU).HasColumnName("HideSKU");
            this.Property(t => t.SKUName).HasColumnName("SKUName");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.SeoMetadataID).HasColumnName("SeoMetadataID");
            this.Property(t => t.ProductSKUMapIIDTEXT).HasColumnName("ProductSKUMapIIDTEXT");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductSKUMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasRequired(t => t.ProductSKUMap1)
                .WithOptional(t => t.ProductSKUMaps1);
            this.HasOptional(t => t.ProductStatu)
                .WithMany(t => t.ProductSKUMaps)
                .HasForeignKey(d => d.StatusID);
            this.HasOptional(t => t.SeoMetadata)
                .WithMany(t => t.ProductSKUMaps)
                .HasForeignKey(d => d.SeoMetadataID);

        }
    }
}
