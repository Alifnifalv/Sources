using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductGalleryMap : EntityTypeConfiguration<vwProductGallery>
    {
        public vwProductGalleryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductGalleryID, t.ProductGalleryZoom, t.ProductGalleryPosition });

            // Properties
            this.Property(t => t.ProductGalleryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.ProductActualImage)
                .HasMaxLength(50);

            this.Property(t => t.ProductListingImage)
                .HasMaxLength(50);

            this.Property(t => t.ProductThumbnail)
                .HasMaxLength(50);

            this.Property(t => t.ProductGalleryImage)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vwProductGallery");
            this.Property(t => t.ProductGalleryID).HasColumnName("ProductGalleryID");
            this.Property(t => t.RefProductGalleryID).HasColumnName("RefProductGalleryID");
            this.Property(t => t.ProductActualImage).HasColumnName("ProductActualImage");
            this.Property(t => t.ProductListingImage).HasColumnName("ProductListingImage");
            this.Property(t => t.ProductThumbnail).HasColumnName("ProductThumbnail");
            this.Property(t => t.ProductGalleryImage).HasColumnName("ProductGalleryImage");
            this.Property(t => t.ProductGalleryZoom).HasColumnName("ProductGalleryZoom");
            this.Property(t => t.ProductGalleryPosition).HasColumnName("ProductGalleryPosition");
        }
    }
}
