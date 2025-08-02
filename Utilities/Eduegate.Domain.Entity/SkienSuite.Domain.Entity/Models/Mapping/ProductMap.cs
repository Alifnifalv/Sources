using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductIID);

            // Properties
            this.Property(t => t.ProductCode)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.ProductName)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Products", "catalog");
            this.Property(t => t.ProductIID).HasColumnName("ProductIID");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.ProductTypeID).HasColumnName("ProductTypeID");
            this.Property(t => t.ProductFamilyID).HasColumnName("ProductFamilyID");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductDescription).HasColumnName("ProductDescription");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.SeoMetadataIID).HasColumnName("SeoMetadataIID");
            this.Property(t => t.Weight).HasColumnName("Weight");
            this.Property(t => t.ManufactureID).HasColumnName("ManufactureID");
            this.Property(t => t.ManufactureCountryID).HasColumnName("ManufactureCountryID");
            this.Property(t => t.UnitGroupID).HasColumnName("UnitGroupID");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.IsMultipleSKUEnabled).HasColumnName("IsMultipleSKUEnabled");
            this.Property(t => t.IsOnline).HasColumnName("IsOnline");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdateBy).HasColumnName("UpdateBy");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Updated).HasColumnName("Updated");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.ProductOwnderID).HasColumnName("ProductOwnderID");

            // Relationships
            this.HasOptional(t => t.Brand)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.BrandID);
            this.HasOptional(t => t.ProductFamily)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ProductFamilyID);
            this.HasOptional(t => t.ProductStatu)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.StatusID);
            this.HasOptional(t => t.SeoMetadata)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.SeoMetadataIID);
            this.HasOptional(t => t.UnitGroup)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.UnitGroupID);
            this.HasOptional(t => t.Unit)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.UnitID);

        }
    }
}
