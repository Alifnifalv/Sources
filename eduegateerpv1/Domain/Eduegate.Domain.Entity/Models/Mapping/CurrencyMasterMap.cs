using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CurrencyMasterMap : EntityTypeConfiguration<CurrencyMaster>
    {
        public CurrencyMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.CurrencyID);

            // Properties
            this.Property(t => t.CurrencyCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CurrencyFullName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CurrencyMaster");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.CurrencyFullName).HasColumnName("CurrencyFullName");
            this.Property(t => t.NoofDecimals).HasColumnName("NoofDecimals");
        }
    }
}
