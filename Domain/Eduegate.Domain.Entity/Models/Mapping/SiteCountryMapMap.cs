using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SiteCountryMapMap : EntityTypeConfiguration<SiteCountryMap>
    {
        public SiteCountryMapMap()
        {
            // Primary Key
            this.HasKey(t => t.SiteCountryMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SiteCountryMaps", "cms");
            this.Property(t => t.SiteCountryMapIID).HasColumnName("SiteCountryMapIID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.IsLocal).HasColumnName("IsLocal");

            // Relationships
            this.HasRequired(t => t.Country)
                .WithMany(t => t.SiteCountryMaps)
                .HasForeignKey(d => d.CountryID);
            this.HasRequired(t => t.Site)
                .WithMany(t => t.SiteCountryMaps)
                .HasForeignKey(d => d.SiteID);

        }
    }
}
