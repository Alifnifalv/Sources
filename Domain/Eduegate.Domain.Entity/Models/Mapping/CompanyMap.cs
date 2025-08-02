using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CompanyMap : EntityTypeConfiguration<Company>
    {
        public CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.CompanyID);

            // Properties
            this.Property(t => t.CompanyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CompanyName)
                .HasMaxLength(100);

            this.Property(t => t.RegistraionNo)
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Companies", "mutual");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.BaseCurrencyID).HasColumnName("BaseCurrencyID");
            this.Property(t => t.LanguageID).HasColumnName("LanguageID");
            this.Property(t => t.RegistraionNo).HasColumnName("RegistraionNo");
            this.Property(t => t.RegistrationDate).HasColumnName("RegistrationDate");
            this.Property(t => t.ExpiryDate).HasColumnName("ExpiryDate");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.CountryID);
            this.HasOptional(t => t.Currency)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.BaseCurrencyID);
            this.HasOptional(t => t.Language)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.LanguageID);
            this.HasOptional(t => t.CompanyStatuses)
                .WithMany(t => t.Companies)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
