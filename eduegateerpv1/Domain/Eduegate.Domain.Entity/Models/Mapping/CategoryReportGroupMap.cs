using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryReportGroupMap : EntityTypeConfiguration<CategoryReportGroup>
    {
        public CategoryReportGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.GroupID);

            // Properties
            this.Property(t => t.GroupName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CategoryReportGroup");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
