using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingBannerMasterMap : EntityTypeConfiguration<MarketingBannerMaster>
    {
        public MarketingBannerMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.MarketingBannerMasterID);

            // Properties
            this.Property(t => t.BannerRequestType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BannerLink)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.BannerDetails)
                .HasMaxLength(400);

            this.Property(t => t.RequestStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("MarketingBannerMaster");
            this.Property(t => t.MarketingBannerMasterID).HasColumnName("MarketingBannerMasterID");
            this.Property(t => t.BannerRequestType).HasColumnName("BannerRequestType");
            this.Property(t => t.RefBannerClassificationID).HasColumnName("RefBannerClassificationID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.BannerLink).HasColumnName("BannerLink");
            this.Property(t => t.BannerDetails).HasColumnName("BannerDetails");
            this.Property(t => t.RequestStatus).HasColumnName("RequestStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.BannerSubmitTill).HasColumnName("BannerSubmitTill");
            this.Property(t => t.RefDesignerID).HasColumnName("RefDesignerID");
            this.Property(t => t.Position).HasColumnName("Position");
        }
    }
}
