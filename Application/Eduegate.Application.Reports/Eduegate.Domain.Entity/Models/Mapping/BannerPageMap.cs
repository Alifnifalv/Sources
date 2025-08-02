using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerPageMap : EntityTypeConfiguration<BannerPage>
    {
        public BannerPageMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerPageID);

            // Properties
            // Table & Column Mappings
            this.ToTable("BannerPage");
            this.Property(t => t.BannerPageID).HasColumnName("BannerPageID");
            this.Property(t => t.BannerID).HasColumnName("BannerID");
            this.Property(t => t.PageID).HasColumnName("PageID");

            // Relationships
            this.HasRequired(t => t.BannerMaster)
                .WithMany(t => t.BannerPages1)
                .HasForeignKey(d => d.BannerID);

        }
    }
}
