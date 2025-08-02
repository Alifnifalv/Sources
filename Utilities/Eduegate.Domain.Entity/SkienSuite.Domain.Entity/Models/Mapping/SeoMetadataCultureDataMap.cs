using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SeoMetadataCultureDataMap : EntityTypeConfiguration<SeoMetadataCultureData>
    {
        public SeoMetadataCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.SEOMetadataID });

            // Properties
            this.Property(t => t.SEOMetadataID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PageTitle)
                .HasMaxLength(900);

            this.Property(t => t.MetaKeywords)
                .HasMaxLength(900);

            this.Property(t => t.MetaDescription)
                .HasMaxLength(900);

            this.Property(t => t.UrlKey)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("SeoMetadataCultureDatas", "mutual");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.SEOMetadataID).HasColumnName("SEOMetadataID");
            this.Property(t => t.PageTitle).HasColumnName("PageTitle");
            this.Property(t => t.MetaKeywords).HasColumnName("MetaKeywords");
            this.Property(t => t.MetaDescription).HasColumnName("MetaDescription");
            this.Property(t => t.UrlKey).HasColumnName("UrlKey");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.SeoMetadataCultureDatas)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.SeoMetadata)
                .WithMany(t => t.SeoMetadataCultureDatas)
                .HasForeignKey(d => d.SEOMetadataID);

        }
    }
}
