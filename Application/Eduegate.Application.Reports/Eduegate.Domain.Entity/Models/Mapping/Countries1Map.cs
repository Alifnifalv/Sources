using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class Countries1Map : EntityTypeConfiguration<Countries1>
    {
        public Countries1Map()
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
            this.ToTable("Countries1", "mutual");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.CountryName).HasColumnName("CountryName");
            this.Property(t => t.TwoLetterCode).HasColumnName("TwoLetterCode");
            this.Property(t => t.ThreeLetterCode).HasColumnName("ThreeLetterCode");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.LanguageID).HasColumnName("LanguageID");

            // Relationships
            this.HasOptional(t => t.Currency)
                .WithMany(t => t.Countries1)
                .HasForeignKey(d => d.CurrencyID);
            this.HasOptional(t => t.Language)
                .WithMany(t => t.Countries1)
                .HasForeignKey(d => d.LanguageID);

        }
    }
}
