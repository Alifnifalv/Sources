using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SeoMetadataMap : EntityTypeConfiguration<SeoMetadata>
    {
        public SeoMetadataMap()
        {
            // Primary Key
            this.HasKey(t => t.SEOMetadataIID);

            // Properties
            this.Property(t => t.PageTitle)
                .HasMaxLength(900);

            this.Property(t => t.MetaKeywords)
                .HasMaxLength(900);

            this.Property(t => t.MetaDescription)
                .HasMaxLength(900);

            this.Property(t => t.UrlKey)
                .HasMaxLength(900);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("SeoMetadatas", "mutual");
            this.Property(t => t.SEOMetadataIID).HasColumnName("SEOMetadataIID");
            this.Property(t => t.PageTitle).HasColumnName("PageTitle");
            this.Property(t => t.MetaKeywords).HasColumnName("MetaKeywords");
            this.Property(t => t.MetaDescription).HasColumnName("MetaDescription");
            this.Property(t => t.UrlKey).HasColumnName("UrlKey");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
