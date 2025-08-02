using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerMap : EntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerIID);

            // Properties
            this.Property(t => t.BannerName)
                .HasMaxLength(100);

            this.Property(t => t.BannerFile)
                .HasMaxLength(255);

            this.Property(t => t.ReferenceID)
                .HasMaxLength(50);

            this.Property(t => t.Link)
                .HasMaxLength(255);

            this.Property(t => t.Target)
                .HasMaxLength(10);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.BannerActionLinkParameters)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("Banners", "cms");
            this.Property(t => t.BannerIID).HasColumnName("BannerIID");
            this.Property(t => t.BannerName).HasColumnName("BannerName");
            this.Property(t => t.BannerFile).HasColumnName("BannerFile");
            this.Property(t => t.BannerTypeID).HasColumnName("BannerTypeID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.Frequency).HasColumnName("Frequency");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.Target).HasColumnName("Target");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.ActionLinkTypeID).HasColumnName("ActionLinkTypeID");
            this.Property(t => t.BannerActionLinkParameters).HasColumnName("BannerActionLinkParameters");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.BannerType)
                .WithMany(t => t.Banners)
                .HasForeignKey(d => d.BannerTypeID);
            this.HasOptional(t => t.BannerStatus)
                .WithMany(t => t.Banners)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
