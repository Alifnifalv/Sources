using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CompanyCurrencyMapMap : EntityTypeConfiguration<CompanyCurrencyMap>
    {
        public CompanyCurrencyMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CompanyID, t.CurrencyID });

            // Properties
            this.Property(t => t.CompanyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CurrencyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CompanyCurrencyMaps", "mutual");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.CompanyCurrencyMaps)
                .HasForeignKey(d => d.CompanyID);
            this.HasRequired(t => t.Currency)
                .WithMany(t => t.CompanyCurrencyMaps)
                .HasForeignKey(d => d.CurrencyID);

        }
    }
}
