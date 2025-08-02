using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WB.Domain.Entity.Models.Mapping
{
    public class CategoryBannerMasterMapMap : EntityTypeConfiguration<CategoryBannerMasterMap>
    {
        public CategoryBannerMasterMapMap()
        {
            // Primary Key
            this.HasKey(t => t.MapID);

            // Properties
            this.Property(t => t.MapArea)
                .HasMaxLength(20);

            this.Property(t => t.MapTitle)
                .HasMaxLength(20);

            this.Property(t => t.MapLink)
                .HasMaxLength(100);

            this.Property(t => t.MapTarget)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CategoryBannerMasterMap");
            this.Property(t => t.MapID).HasColumnName("MapID");
            this.Property(t => t.RefBannerID).HasColumnName("RefBannerID");
            this.Property(t => t.MapArea).HasColumnName("MapArea");
            this.Property(t => t.MapTitle).HasColumnName("MapTitle");
            this.Property(t => t.MapLink).HasColumnName("MapLink");
            this.Property(t => t.MapTarget).HasColumnName("MapTarget");
        }
    }
}
