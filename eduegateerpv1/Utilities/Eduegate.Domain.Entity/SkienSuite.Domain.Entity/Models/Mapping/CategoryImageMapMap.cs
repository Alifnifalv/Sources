using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryImageMapMap : EntityTypeConfiguration<CategoryImageMap>
    {
        public CategoryImageMapMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryImageMapIID);

            // Properties
            this.Property(t => t.ImageFile)
                .HasMaxLength(200);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.ImageLink)
                .HasMaxLength(400);

            this.Property(t => t.ImageTarget)
                .HasMaxLength(200);

            this.Property(t => t.ImageLinkParameters)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("CategoryImageMaps", "catalog");
            this.Property(t => t.CategoryImageMapIID).HasColumnName("CategoryImageMapIID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.ImageTypeID).HasColumnName("ImageTypeID");
            this.Property(t => t.ImageFile).HasColumnName("ImageFile");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.ImageTitle).HasColumnName("ImageTitle");
            this.Property(t => t.ImageLink).HasColumnName("ImageLink");
            this.Property(t => t.ImageTarget).HasColumnName("ImageTarget");
            this.Property(t => t.ActionLinkTypeID).HasColumnName("ActionLinkTypeID");
            this.Property(t => t.ImageLinkParameters).HasColumnName("ImageLinkParameters");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.CategoryImageMaps)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.ImageType)
                .WithMany(t => t.CategoryImageMaps)
                .HasForeignKey(d => d.ImageTypeID);

        }
    }
}
