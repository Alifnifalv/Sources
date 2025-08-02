using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LanguageMap : EntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            // Primary Key
            this.HasKey(t => t.LanguageID);

            // Properties
            this.Property(t => t.LanguageID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Language1)
                .HasMaxLength(50);

            this.Property(t => t.LanguageCodeTwoLetter)
                .HasMaxLength(2);

            this.Property(t => t.LanguageCodeThreeLetter)
                .HasMaxLength(3);

            //this.Property(t => t.CultureCode)
            //    .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Languages", "mutual");
            this.Property(t => t.LanguageID).HasColumnName("LanguageID");
            this.Property(t => t.Language1).HasColumnName("Language");
            this.Property(t => t.LanguageCodeTwoLetter).HasColumnName("LanguageCodeTwoLetter");
            this.Property(t => t.LanguageCodeThreeLetter).HasColumnName("LanguageCodeThreeLetter");
            //this.Property(t => t.CultureCode).HasColumnName("CultureCode");
            this.Property(t => t.IsEnabled).HasColumnName("IsEnabled");
            this.Property(t => t.CultureID).HasColumnName("CultureID");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Languages)
                .HasForeignKey(d => d.CountryID);

            this.HasOptional(t => t.Culture)
                .WithMany(t => t.Languages)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
