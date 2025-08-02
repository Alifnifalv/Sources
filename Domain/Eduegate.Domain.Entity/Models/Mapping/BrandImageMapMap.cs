using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BrandImageMapMap : EntityTypeConfiguration<BrandImageMap>
    {
        public BrandImageMapMap()
        {
            // Primary Key
            this.HasKey(t => t.BrandImageMapIID);

            // Properties
            this.Property(t => t.ImageFile)
                .HasMaxLength(200);

            this.Property(t => t.ImageTitle)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BrandImageMap", "catalog");
            this.Property(t => t.BrandImageMapIID).HasColumnName("BrandImageMapIID");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.ImageTypeID).HasColumnName("ImageTypeID");
            this.Property(t => t.ImageFile).HasColumnName("ImageFile");
            this.Property(t => t.ImageTitle).HasColumnName("ImageTitle");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Brand)
                .WithMany(t => t.BrandImageMaps)
                .HasForeignKey(d => d.BrandID);

        }
    }
}
