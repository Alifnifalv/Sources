using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingBannerLogMap : EntityTypeConfiguration<MarketingBannerLog>
    {
        public MarketingBannerLogMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingBannerLogID);

            // Properties
            this.Property(t => t.RequestStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("MarketingBannerLog");
            this.Property(t => t.MarketingBannerLogID).HasColumnName("MarketingBannerLogID");
            this.Property(t => t.RefMarketingBannerDetailsID).HasColumnName("RefMarketingBannerDetailsID");
            this.Property(t => t.RequestStatus).HasColumnName("RequestStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
