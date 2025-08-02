using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerSearchViewMap : EntityTypeConfiguration<BannerSearchView>
    {
        public BannerSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerIID);

            // Properties
            this.Property(t => t.BannerIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BannerName)
                .HasMaxLength(100);

            this.Property(t => t.BannerFile)
                .HasMaxLength(255);

            this.Property(t => t.Link)
                .HasMaxLength(255);

            this.Property(t => t.Target)
                .HasMaxLength(10);

            this.Property(t => t.BannerStatusName)
                .HasMaxLength(100);

            this.Property(t => t.ActualImage)
                .HasMaxLength(263);

            this.Property(t => t.BannerTypeName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BannerSearchView", "cms");
            this.Property(t => t.BannerIID).HasColumnName("BannerIID");
            this.Property(t => t.BannerName).HasColumnName("BannerName");
            this.Property(t => t.BannerFile).HasColumnName("BannerFile");
            this.Property(t => t.BannerTypeID).HasColumnName("BannerTypeID");
            this.Property(t => t.Frequency).HasColumnName("Frequency");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.Target).HasColumnName("Target");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.BannerStatusName).HasColumnName("BannerStatusName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.ActualImage).HasColumnName("ActualImage");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.BannerTypeName).HasColumnName("BannerTypeName");
        }
    }
}
