using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductGalleryMap : EntityTypeConfiguration<ProductGallery>
    {
        public ProductGalleryMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductGalleryID);

            // Properties
            this.Property(t => t.ProductActualImage)
                .HasMaxLength(50);

            this.Property(t => t.ProductListingImage)
                .HasMaxLength(50);

            this.Property(t => t.ProductThumbnail)
                .HasMaxLength(50);

            this.Property(t => t.ProductGalleryImage)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductGallery");
            this.Property(t => t.ProductGalleryID).HasColumnName("ProductGalleryID");
            this.Property(t => t.RefProductGalleryID).HasColumnName("RefProductGalleryID");
            this.Property(t => t.ProductActualImage).HasColumnName("ProductActualImage");
            this.Property(t => t.ProductListingImage).HasColumnName("ProductListingImage");
            this.Property(t => t.ProductThumbnail).HasColumnName("ProductThumbnail");
            this.Property(t => t.ProductGalleryImage).HasColumnName("ProductGalleryImage");
            this.Property(t => t.ProductGalleryZoom).HasColumnName("ProductGalleryZoom");
            this.Property(t => t.ProductGalleryPosition).HasColumnName("ProductGalleryPosition");

            // Relationships
            //this.HasOptional(t => t.ProductMaster)
            //    .WithMany(t => t.ProductGalleries)
                //.HasForeignKey(d => d.RefProductGalleryID);

        }
    }
}
