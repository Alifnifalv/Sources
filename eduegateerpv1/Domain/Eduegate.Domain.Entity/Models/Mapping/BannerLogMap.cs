using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerLogMap : EntityTypeConfiguration<BannerLog>
    {
        public BannerLogMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerLogID);

            // Properties
            this.Property(t => t.BannerAction)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.IPAddress)
                .HasMaxLength(255);

            this.Property(t => t.IPLocation)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("BannerLog");
            this.Property(t => t.BannerLogID).HasColumnName("BannerLogID");
            this.Property(t => t.BannerID).HasColumnName("BannerID");
            this.Property(t => t.BannerAction).HasColumnName("BannerAction");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.IPLocation).HasColumnName("IPLocation");
            this.Property(t => t.LogTime).HasColumnName("LogTime");

            // Relationships
            this.HasOptional(t => t.BannerMaster)
                .WithMany(t => t.BannerLogs)
                .HasForeignKey(d => d.BannerID);

        }
    }
}
