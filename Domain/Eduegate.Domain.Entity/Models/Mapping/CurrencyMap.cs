using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CurrencyMap : EntityTypeConfiguration<Currency>
    {
        public CurrencyMap()
        {
            // Primary Key
            this.HasKey(t => t.CurrencyID);

            // Properties
            this.Property(t => t.CurrencyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ISOCode)
                .HasMaxLength(20);

            this.Property(t => t.AnsiCode)
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.Symbol)
                .HasMaxLength(5);

            this.Property(t => t.DisplayCode)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Currencies", "mutual");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.ISOCode).HasColumnName("ISOCode");
            this.Property(t => t.AnsiCode).HasColumnName("AnsiCode");
            this.Property(t => t.NumericCode).HasColumnName("NumericCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Symbol).HasColumnName("Symbol");
            this.Property(t => t.DecimalPrecisions).HasColumnName("DecimalPrecisions");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.IsEnabled).HasColumnName("IsEnabled");
            this.Property(t => t.DisplayCode).HasColumnName("DisplayCode");

            // Relationships
            this.HasOptional(t => t.Company)
                .WithMany(t => t.Currencies)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
