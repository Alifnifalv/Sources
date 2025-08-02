using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SiteCountryMaps1Map : EntityTypeConfiguration<SiteCountryMaps1>
    {
        public SiteCountryMaps1Map()
        {
            // Primary Key
            this.HasKey(t => t.SiteCountryMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SiteCountryMaps", "mutual");
            this.Property(t => t.SiteCountryMapIID).HasColumnName("SiteCountryMapIID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
        }
    }
}
