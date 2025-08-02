using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSearchKeywordsArIntlMap : EntityTypeConfiguration<ProductSearchKeywordsArIntl>
    {
        public ProductSearchKeywordsArIntlMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.KeywordAr)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ProductSearchKeywordsArIntl");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.KeywordAr).HasColumnName("KeywordAr");
            this.Property(t => t.Preference).HasColumnName("Preference");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
        }
    }
}
