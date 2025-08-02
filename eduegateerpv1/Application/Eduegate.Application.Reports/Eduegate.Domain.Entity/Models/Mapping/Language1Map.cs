using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class Language1Map : EntityTypeConfiguration<Language1>
    {
        public Language1Map()
        {
            // Primary Key
            this.HasKey(t => t.LanguageID);

            // Properties
            this.Property(t => t.LanguageID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Language)
                .HasMaxLength(50);

            this.Property(t => t.LanguageCodeTwoLetter)
                .HasMaxLength(2);

            this.Property(t => t.LanguageCodeThreeLetter)
                .HasMaxLength(3);

            this.Property(t => t.CultureCode)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Languages", "mutual");
            this.Property(t => t.LanguageID).HasColumnName("LanguageID");
            this.Property(t => t.Language).HasColumnName("Language");
            this.Property(t => t.LanguageCodeTwoLetter).HasColumnName("LanguageCodeTwoLetter");
            this.Property(t => t.LanguageCodeThreeLetter).HasColumnName("LanguageCodeThreeLetter");
            this.Property(t => t.CultureCode).HasColumnName("CultureCode");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.IsEnabled).HasColumnName("IsEnabled");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Languages)
                .HasForeignKey(d => d.CountryID);
        }
    }
}
