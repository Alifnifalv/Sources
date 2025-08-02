using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingBannerCommentMap : EntityTypeConfiguration<MarketingBannerComment>
    {
        public MarketingBannerCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingBannerCommentID);

            // Properties
            this.Property(t => t.BannerComments)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("MarketingBannerComment");
            this.Property(t => t.MarketingBannerCommentID).HasColumnName("MarketingBannerCommentID");
            this.Property(t => t.RefMarketingBannerMasterID).HasColumnName("RefMarketingBannerMasterID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.BannerComments).HasColumnName("BannerComments");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
