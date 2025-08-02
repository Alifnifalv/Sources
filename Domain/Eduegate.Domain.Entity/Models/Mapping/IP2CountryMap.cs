using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IP2CountryMap : EntityTypeConfiguration<IP2Country>
    {
        public IP2CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.IP2CountryID);

            // Properties
            this.Property(t => t.TwoCountryCode)
                .HasMaxLength(255);

            this.Property(t => t.ThreeCountryCode)
                .HasMaxLength(255);

            this.Property(t => t.CountryName)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("IP2Country");
            this.Property(t => t.BeginningIP).HasColumnName("BeginningIP");
            this.Property(t => t.EndingIP).HasColumnName("EndingIP");
            this.Property(t => t.AssignedIP).HasColumnName("AssignedIP");
            this.Property(t => t.TwoCountryCode).HasColumnName("TwoCountryCode");
            this.Property(t => t.ThreeCountryCode).HasColumnName("ThreeCountryCode");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
            this.Property(t => t.IP2CountryID).HasColumnName("IP2CountryID");
        }
    }
}
