using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerStatusMap : EntityTypeConfiguration<BannerStatus>
    {
        public BannerStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerStatusID);

            // Properties
            this.Property(t => t.BannerStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BannerStatusName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BannerStatuses", "cms");
            this.Property(t => t.BannerStatusID).HasColumnName("BannerStatusID");
            this.Property(t => t.BannerStatusName).HasColumnName("BannerStatusName");
        }
    }
}
