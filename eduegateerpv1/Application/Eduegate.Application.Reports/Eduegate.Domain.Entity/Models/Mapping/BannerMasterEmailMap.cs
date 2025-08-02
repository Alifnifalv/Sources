using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerMasterEmailMap : EntityTypeConfiguration<BannerMasterEmail>
    {
        public BannerMasterEmailMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerID);

            // Properties
            this.Property(t => t.BannerName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BannerFile)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Link)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("BannerMasterEmail");
            this.Property(t => t.BannerID).HasColumnName("BannerID");
            this.Property(t => t.BannerName).HasColumnName("BannerName");
            this.Property(t => t.BannerFile).HasColumnName("BannerFile");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
