using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSKUTagMap : EntityTypeConfiguration<ProductSKUTag>
    {
        public ProductSKUTagMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSKUTagIID);

            // Properties
            this.Property(t => t.TagName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductSKUTags", "catalog");
            this.Property(t => t.ProductSKUTagIID).HasColumnName("ProductSKUTagIID");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
