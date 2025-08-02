using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductVideoMapMap : EntityTypeConfiguration<ProductVideoMap>
    {
        public ProductVideoMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductVideoMapIID);

            this.Property(t => t.VideoFile)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductVideoMaps", "catalog");
            this.Property(t => t.ProductVideoMapIID).HasColumnName("ProductVideoMapIID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.VideoFile).HasColumnName("VideoFile");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductVideoMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductVideoMaps)
                .HasForeignKey(d => d.ProductSKUMapID);

        }
    }
}
