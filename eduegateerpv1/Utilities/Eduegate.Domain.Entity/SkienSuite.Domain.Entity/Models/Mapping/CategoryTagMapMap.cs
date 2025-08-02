using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryTagMapMap : EntityTypeConfiguration<CategoryTagMap>
    {
        public CategoryTagMapMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryTagMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CategoryTagMaps", "catalog");
            this.Property(t => t.CategoryTagMapIID).HasColumnName("CategoryTagMapIID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.CategoryTagID).HasColumnName("CategoryTagID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.CategoryTagMaps)
                .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.CategoryTag)
                .WithMany(t => t.CategoryTagMaps)
                .HasForeignKey(d => d.CategoryTagID);

        }
    }
}
