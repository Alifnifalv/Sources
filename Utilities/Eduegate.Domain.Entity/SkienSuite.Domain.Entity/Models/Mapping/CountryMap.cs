using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryID);

            // Properties
            this.Property(t => t.CountryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CountryName)
                .HasMaxLength(50);

            this.Property(t => t.TwoLetterCode)
                .HasMaxLength(2);

            this.Property(t => t.ThreeLetterCode)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Countries", "mutual");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
            this.Property(t => t.TwoLetterCode).HasColumnName("TwoLetterCode");
            this.Property(t => t.ThreeLetterCode).HasColumnName("ThreeLetterCode");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.LanguageID).HasColumnName("LanguageID");

            // Relationships
            this.HasMany(t => t.ServiceProviderCountryGroups)
                .WithMany(t => t.Countries)
                .Map(m =>
                    {
                        m.ToTable("ServiceProviderCountryGroupMaps", "distribution");
                        m.MapLeftKey("CountryID");
                        m.MapRightKey("CountryGroupID");
                    });

            this.HasOptional(t => t.Currency)
                .WithMany(t => t.Countries)
                .HasForeignKey(d => d.CurrencyID);
            this.HasOptional(t => t.Language)
                .WithMany(t => t.Countries)
                .HasForeignKey(d => d.LanguageID);

        }
    }
}
