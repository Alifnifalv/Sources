using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryTagMap : EntityTypeConfiguration<Eduegate.Domain.Entity.Models.CategoryTag>
    {
        public CategoryTagMap()
        {
            // Primary Key
            // Primary Key
            this.HasKey(t => t.CategoryTagIID);

            // Properties
            this.Property(t => t.TagName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CategoryTags", "catalog");
            this.Property(t => t.CategoryTagIID).HasColumnName("CategoryTagIID");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
