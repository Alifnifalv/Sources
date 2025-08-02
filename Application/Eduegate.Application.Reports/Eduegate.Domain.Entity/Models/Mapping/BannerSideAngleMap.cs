using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerSideAngleMap : EntityTypeConfiguration<BannerSideAngle>
    {
        public BannerSideAngleMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerSideAngleID);

            // Properties
            this.Property(t => t.BannerSideAngleName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BannerSideAngleFile)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BannerSideAnglePosition)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.BannerSideAngleUrl)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BannerSideAngleTarget)
                .HasMaxLength(10);

            this.Property(t => t.BannerSideAngleNameAr)
                .HasMaxLength(255);

            this.Property(t => t.Lang)
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("BannerSideAngle");
            this.Property(t => t.BannerSideAngleID).HasColumnName("BannerSideAngleID");
            this.Property(t => t.BannerSideAngleName).HasColumnName("BannerSideAngleName");
            this.Property(t => t.BannerSideAngleFile).HasColumnName("BannerSideAngleFile");
            this.Property(t => t.BannerSideAnglePosition).HasColumnName("BannerSideAnglePosition");
            this.Property(t => t.BannerSideAngleUrl).HasColumnName("BannerSideAngleUrl");
            this.Property(t => t.BannerSideAngleTarget).HasColumnName("BannerSideAngleTarget");
            this.Property(t => t.BannerSideAngleStatus).HasColumnName("BannerSideAngleStatus");
            this.Property(t => t.BannerSideAngleNameAr).HasColumnName("BannerSideAngleNameAr");
            this.Property(t => t.Lang).HasColumnName("Lang");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
