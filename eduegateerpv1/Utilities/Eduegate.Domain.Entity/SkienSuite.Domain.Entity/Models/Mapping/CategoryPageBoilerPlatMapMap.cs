using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CategoryPageBoilerPlatMapMap : EntityTypeConfiguration<CategoryPageBoilerPlatMap>
    {
        public CategoryPageBoilerPlatMapMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryPageBoilerPlatMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CategoryPageBoilerPlatMaps", "cms");
            this.Property(t => t.CategoryPageBoilerPlatMapIID).HasColumnName("CategoryPageBoilerPlatMapIID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.PageBoilerplateMapID).HasColumnName("PageBoilerplateMapID");
            this.Property(t => t.SerialNumber).HasColumnName("SerialNumber");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.PageBoilerplateMap)
                .WithMany(t => t.CategoryPageBoilerPlatMaps)
                .HasForeignKey(d => d.PageBoilerplateMapID);

        }
    }
}
