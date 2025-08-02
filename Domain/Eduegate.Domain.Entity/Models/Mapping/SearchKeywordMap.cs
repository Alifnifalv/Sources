using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SearchKeywordMap : EntityTypeConfiguration<SearchKeyword>
    {
        public SearchKeywordMap()
        {
            // Primary Key
            this.HasKey(t => t.SearchKeyWordID);

            // Properties
            this.Property(t => t.Keyword)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.SortingValue)
                .HasMaxLength(255);

            this.Property(t => t.CategoryOrBrandName)
                .HasMaxLength(255);

            this.Property(t => t.SessionID)
                .HasMaxLength(50);

            this.Property(t => t.IPAddress)
                .HasMaxLength(50);

            this.Property(t => t.IPCountry)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SearchKeywords");
            this.Property(t => t.SearchKeyWordID).HasColumnName("SearchKeyWordID");
            this.Property(t => t.Keyword).HasColumnName("Keyword");
            this.Property(t => t.SearchOn).HasColumnName("SearchOn");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.ResultCount).HasColumnName("ResultCount");
            this.Property(t => t.ResultPage).HasColumnName("ResultPage");
            this.Property(t => t.SearchSql).HasColumnName("SearchSql");
            this.Property(t => t.IsCategory).HasColumnName("IsCategory");
            this.Property(t => t.SortingValue).HasColumnName("SortingValue");
            this.Property(t => t.CategoryOrBrandName).HasColumnName("CategoryOrBrandName");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.IPCountry).HasColumnName("IPCountry");
        }
    }
}
