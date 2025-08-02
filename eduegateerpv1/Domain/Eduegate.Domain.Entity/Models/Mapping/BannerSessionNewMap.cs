using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerSessionNewMap : EntityTypeConfiguration<BannerSessionNew>
    {
        public BannerSessionNewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BannerID, t.SessionID, t.UniqueID, t.LoopID, t.BannerTime });

            // Properties
            this.Property(t => t.BannerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.UniqueID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LoopID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("BannerSessionNew");
            this.Property(t => t.BannerID).HasColumnName("BannerID");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.UniqueID).HasColumnName("UniqueID");
            this.Property(t => t.LoopID).HasColumnName("LoopID");
            this.Property(t => t.BannerTime).HasColumnName("BannerTime");
        }
    }
}
