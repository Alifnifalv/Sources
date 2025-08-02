using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductListingVisitLogIntlMap : EntityTypeConfiguration<ProductListingVisitLogIntl>
    {
        public ProductListingVisitLogIntlMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.SortOrder)
                .HasMaxLength(100);

            this.Property(t => t.FixedFilters)
                .HasMaxLength(300);

            this.Property(t => t.UserFilters)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ProductListingVisitLogIntl");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.isCategory).HasColumnName("isCategory");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.isBrand).HasColumnName("isBrand");
            this.Property(t => t.BrandID).HasColumnName("BrandID");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.PageIndex).HasColumnName("PageIndex");
            this.Property(t => t.NumRows).HasColumnName("NumRows");
            this.Property(t => t.isFixedFilter).HasColumnName("isFixedFilter");
            this.Property(t => t.isUserFilter).HasColumnName("isUserFilter");
            this.Property(t => t.FixedFilters).HasColumnName("FixedFilters");
            this.Property(t => t.UserFilters).HasColumnName("UserFilters");
            this.Property(t => t.VisitOn).HasColumnName("VisitOn");
        }
    }
}
