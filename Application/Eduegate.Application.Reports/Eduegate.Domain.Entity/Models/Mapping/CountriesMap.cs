using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CountriesMap : EntityTypeConfiguration<Country>
    {
        public CountriesMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryIID);

            // Properties
            this.Property(t => t.CountryCode)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.CountryName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CurrencyName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CurrencyCode)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Countries", "mutual");
            this.Property(t => t.CountryIID).HasColumnName("CountryIID");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.CurrencyName).HasColumnName("CurrencyName");
            this.Property(t => t.ConversionRate).HasColumnName("ConversionRate");
            this.Property(t => t.DecimalPlaces).HasColumnName("DecimalPlaces");
            this.Property(t => t.DataFeedDateTime).HasColumnName("DataFeedDateTime");
            this.Property(t => t.IsActiveForCurrency).HasColumnName("IsActiveForCurrency");
            this.Property(t => t.CurrencyCodeDisplayText).HasColumnName("CurrencyCodeDisplayText");

        }
    }
}
