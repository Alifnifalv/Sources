using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductTagMapMap : EntityTypeConfiguration<Eduegate.Domain.Entity.Models.ProductTagMap>
    {
        public ProductTagMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductTagMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductTagMaps", "catalog");
            this.Property(t => t.ProductTagMapIID).HasColumnName("ProductTagMapIID");
            this.Property(t => t.ProductTagID).HasColumnName("ProductTagID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductTagMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductTag)
                .WithMany(t => t.ProductTagMaps)
                .HasForeignKey(d => d.ProductTagID);

        }
    }
}
