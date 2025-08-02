using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryReportGroupDetailMap : EntityTypeConfiguration<CategoryReportGroupDetail>
    {
        public CategoryReportGroupDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CategoryReportGroupDetails");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.RefGroupID).HasColumnName("RefGroupID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
        }
    }
}
