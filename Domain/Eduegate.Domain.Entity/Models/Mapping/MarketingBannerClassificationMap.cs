using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingBannerClassificationMap : EntityTypeConfiguration<MarketingBannerClassification>
    {
        public MarketingBannerClassificationMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerClassificationID);

            // Properties
            this.Property(t => t.ClassificationType)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MarketingBannerClassification");
            this.Property(t => t.BannerClassificationID).HasColumnName("BannerClassificationID");
            this.Property(t => t.ClassificationType).HasColumnName("ClassificationType");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
