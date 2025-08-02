using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductImageMapMap : EntityTypeConfiguration<ProductImageMap>
    {
        public ProductImageMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductImageMapIID);

            // Properties
            this.Property(t => t.ImageFile)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductImageMaps", "catalog");
            this.Property(t => t.ProductImageMapIID).HasColumnName("ProductImageMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.ProductImageTypeID).HasColumnName("ProductImageTypeID");
            this.Property(t => t.ImageFile).HasColumnName("ImageFile");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductImageMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductImageMaps)
                .HasForeignKey(d => d.ProductSKUMapID);
            //this.HasOptional(t => t.Property)
            //    .WithMany(t => t.ProductImageMaps)
            //    .HasForeignKey(d => d.ProductImageTypeID);
        }
    }
}
