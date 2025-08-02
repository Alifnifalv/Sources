using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerMasterMap : EntityTypeConfiguration<BannerMaster>
    {
        public BannerMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerID);

            // Properties
            this.Property(t => t.BannerName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BannerFile)
                .HasMaxLength(255);

            this.Property(t => t.BannerType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Position)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Link)
                .HasMaxLength(255);

            this.Property(t => t.Target)
                .HasMaxLength(10);

            this.Property(t => t.BannerNameAr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("BannerMaster");
            this.Property(t => t.BannerID).HasColumnName("BannerID");
            this.Property(t => t.BannerName).HasColumnName("BannerName");
            this.Property(t => t.BannerFile).HasColumnName("BannerFile");
            this.Property(t => t.BannerType).HasColumnName("BannerType");
            this.Property(t => t.Frequency).HasColumnName("Frequency");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.Target).HasColumnName("Target");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.BannerNameAr).HasColumnName("BannerNameAr");
        }
    }
}
