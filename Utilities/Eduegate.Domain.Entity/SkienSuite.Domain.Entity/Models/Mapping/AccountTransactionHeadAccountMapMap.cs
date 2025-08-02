using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AccountTransactionHeadAccountMapMap : EntityTypeConfiguration<AccountTransactionHeadAccountMap>
    {
        public AccountTransactionHeadAccountMapMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountTransactionHeadAccountMapIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AccountTransactionHeadAccountMaps", "account");
            this.Property(t => t.AccountTransactionHeadAccountMapIID).HasColumnName("AccountTransactionHeadAccountMapIID");
            this.Property(t => t.AccountTransactionID).HasColumnName("AccountTransactionID");
            this.Property(t => t.AccountTransactionHeadID).HasColumnName("AccountTransactionHeadID");

            // Relationships
            this.HasRequired(t => t.AccountTransactionHead)
                .WithMany(t => t.AccountTransactionHeadAccountMaps)
                .HasForeignKey(d => d.AccountTransactionHeadID);
            this.HasRequired(t => t.AccountTransaction)
                .WithMany(t => t.AccountTransactionHeadAccountMaps)
                .HasForeignKey(d => d.AccountTransactionID);

        }
    }
}
