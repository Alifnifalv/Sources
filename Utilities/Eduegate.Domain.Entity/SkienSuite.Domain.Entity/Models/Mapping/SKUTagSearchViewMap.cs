using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SKUTagSearchViewMap : EntityTypeConfiguration<SKUTagSearchView>
    {
        public SKUTagSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSKUTagIID);

            // Properties
            this.Property(t => t.TagName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SKUTagSearchView", "catalog");
            this.Property(t => t.ProductSKUTagIID).HasColumnName("ProductSKUTagIID");
            this.Property(t => t.TagName).HasColumnName("TagName");
        }
    }
}
