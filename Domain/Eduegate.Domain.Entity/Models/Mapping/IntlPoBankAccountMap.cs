using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoBankAccountMap : EntityTypeConfiguration<IntlPoBankAccount>
    {
        public IntlPoBankAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoBankAccountID);

            // Properties
            this.Property(t => t.BankAccount)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IntlPoBankAccount");
            this.Property(t => t.IntlPoBankAccountID).HasColumnName("IntlPoBankAccountID");
            this.Property(t => t.BankAccount).HasColumnName("BankAccount");
            this.Property(t => t.BankAccountActive).HasColumnName("BankAccountActive");
        }
    }
}
