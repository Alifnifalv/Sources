using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingBannerConfigMap : EntityTypeConfiguration<MarketingBannerConfig>
    {
        public MarketingBannerConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingBannerConfigID);

            // Properties
            this.Property(t => t.FormatText)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UDF1)
                .HasMaxLength(50);

            this.Property(t => t.UDF2)
                .HasMaxLength(50);

            this.Property(t => t.UDF3)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MarketingBannerConfig");
            this.Property(t => t.MarketingBannerConfigID).HasColumnName("MarketingBannerConfigID");
            this.Property(t => t.FormatText).HasColumnName("FormatText");
            this.Property(t => t.MaxWidth).HasColumnName("MaxWidth");
            this.Property(t => t.MaxHeight).HasColumnName("MaxHeight");
            this.Property(t => t.MaxSizeKB).HasColumnName("MaxSizeKB");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.EnableStats).HasColumnName("EnableStats");
            this.Property(t => t.UDF1).HasColumnName("UDF1");
            this.Property(t => t.UDF2).HasColumnName("UDF2");
            this.Property(t => t.UDF3).HasColumnName("UDF3");
        }
    }
}
