using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ZoneSearchViewMap : EntityTypeConfiguration<ZoneSearchView>
    {
        public ZoneSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ZoneID);

            // Properties
            this.Property(t => t.ZoneID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ZoneName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ZoneSearchView", "mutual");
            this.Property(t => t.ZoneID).HasColumnName("ZoneID");
            this.Property(t => t.ZoneName).HasColumnName("ZoneName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
