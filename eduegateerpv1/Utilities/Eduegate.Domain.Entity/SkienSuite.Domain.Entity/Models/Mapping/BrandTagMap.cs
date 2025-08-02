using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BrandTagMap : EntityTypeConfiguration<BrandTag>
    {
        public BrandTagMap()
        {
            // Primary Key
            this.HasKey(t => t.BrandTagIID);

            // Properties
            this.Property(t => t.TagName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BrandTags", "catalog");
            this.Property(t => t.BrandTagIID).HasColumnName("BrandTagIID");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Brand)
                .WithMany(t => t.BrandTags)
                .HasForeignKey(d => d.BrandID);

        }
    }
}
