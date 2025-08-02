using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUTagMapMap : EntityTypeConfiguration<Eduegate.Domain.Entity.Models.ProductSKUTagMap>
    {
        public ProductSKUTagMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSKUTagMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductSKUTagMaps", "catalog");
            this.Property(t => t.ProductSKUTagMapIID).HasColumnName("ProductSKUTagMapIID");
            this.Property(t => t.ProductSKUTagID).HasColumnName("ProductSKUTagID");
            this.Property(t => t.ProductSKuMapID).HasColumnName("ProductSKuMapID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductSKUTagMaps)
                .HasForeignKey(d => d.ProductSKuMapID);
            this.HasOptional(t => t.ProductSKUTag)
                .WithMany(t => t.ProductSKUTagMaps)
                .HasForeignKey(d => d.ProductSKUTagID);

        }
    }
}
