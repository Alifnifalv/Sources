using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingBannerDetailMap : EntityTypeConfiguration<MarketingBannerDetail>
    {
        public MarketingBannerDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingBannerDetailsID);

            // Properties
            this.Property(t => t.BannerImageName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("MarketingBannerDetails");
            this.Property(t => t.MarketingBannerDetailsID).HasColumnName("MarketingBannerDetailsID");
            this.Property(t => t.RefMarketingBannerMasterID).HasColumnName("RefMarketingBannerMasterID");
            this.Property(t => t.RefMarketingBannerConfigID).HasColumnName("RefMarketingBannerConfigID");
            this.Property(t => t.BannerImageName).HasColumnName("BannerImageName");
            this.Property(t => t.RefDesignerID).HasColumnName("RefDesignerID");
            this.Property(t => t.UDFValue1).HasColumnName("UDFValue1");
            this.Property(t => t.UDFValue2).HasColumnName("UDFValue2");
            this.Property(t => t.UDFValue3).HasColumnName("UDFValue3");
        }
    }
}
