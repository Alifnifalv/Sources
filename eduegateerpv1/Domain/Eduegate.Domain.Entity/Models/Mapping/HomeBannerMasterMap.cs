using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class HomeBannerMasterMap : EntityTypeConfiguration<HomeBannerMaster>
    {
        public HomeBannerMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerID);

            // Properties
            this.Property(t => t.BannerName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.ThumbFile)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.BannerFile)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Link)
                .HasMaxLength(255);

            this.Property(t => t.Target)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("HomeBannerMaster");
            this.Property(t => t.BannerID).HasColumnName("BannerID");
            this.Property(t => t.BannerName).HasColumnName("BannerName");
            this.Property(t => t.ThumbFile).HasColumnName("ThumbFile");
            this.Property(t => t.BannerFile).HasColumnName("BannerFile");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.Target).HasColumnName("Target");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.UseMap).HasColumnName("UseMap");
        }
    }
}
