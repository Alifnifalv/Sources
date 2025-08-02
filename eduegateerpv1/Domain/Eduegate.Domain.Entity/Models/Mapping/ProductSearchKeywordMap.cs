using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSearchKeywordMap : EntityTypeConfiguration<ProductSearchKeyword>
    {
        public ProductSearchKeywordMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.Keyword)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ProductSearchKeywords");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.Keyword).HasColumnName("Keyword");
            this.Property(t => t.Preference).HasColumnName("Preference");
        }
    }
}
