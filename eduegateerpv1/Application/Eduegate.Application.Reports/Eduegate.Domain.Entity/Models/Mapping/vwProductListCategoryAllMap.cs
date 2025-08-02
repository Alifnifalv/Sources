using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductListCategoryAllMap : EntityTypeConfiguration<vwProductListCategoryAll>
    {
        public vwProductListCategoryAllMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RefProductCategoryProductID, t.CategoryID });

            // Properties
            this.Property(t => t.RefProductCategoryProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefCategoryBreadcrumbs)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vwProductListCategoryAll");
            this.Property(t => t.RefProductCategoryProductID).HasColumnName("RefProductCategoryProductID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.RefCategoryBreadcrumbs).HasColumnName("RefCategoryBreadcrumbs");
        }
    }
}
