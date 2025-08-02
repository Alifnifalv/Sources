using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BrandMap : EntityTypeConfiguration<Brand>
    {
        public BrandMap()
        {
            // Primary Key
            this.HasKey(t => t.BrandIID);

            // Properties
            this.Property(t => t.BrandCode)
                .HasMaxLength(50);

            this.Property(t => t.BrandName)
                .HasMaxLength(50);

            this.Property(t => t.LogoFile)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Brands", "catalog");
            this.Property(t => t.BrandIID).HasColumnName("BrandIID");
            this.Property(t => t.BrandCode).HasColumnName("BrandCode");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.Descirption).HasColumnName("Descirption");
            this.Property(t => t.LogoFile).HasColumnName("LogoFile");
            this.Property(t => t.IsIncludeHomePage).HasColumnName("IsIncludeHomePage");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.SEOMetadataID).HasColumnName("SEOMetadataID");

            // Relationships
           this.HasOptional(t => t.BrandStatus)
                .WithMany(t => t.Brands)
                .HasForeignKey(d => d.StatusID);
           this.HasOptional(t => t.SeoMetadata)
              .WithMany(t => t.Brands)
              .HasForeignKey(d => d.SEOMetadataID);

        }
    }
}
