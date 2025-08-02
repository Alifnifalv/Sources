using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BannerMasterPositionMap : EntityTypeConfiguration<BannerMasterPosition>
    {
        public BannerMasterPositionMap()
        {
            // Primary Key
            this.HasKey(t => t.RefBannerID);

            // Properties
            this.Property(t => t.RefBannerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("BannerMasterPosition");
            this.Property(t => t.RefBannerID).HasColumnName("RefBannerID");
            this.Property(t => t.Position).HasColumnName("Position");
        }
    }
}
