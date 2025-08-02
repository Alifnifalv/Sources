using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AccountTransactionInventoryHeadMapMap : EntityTypeConfiguration<AccountTransactionInventoryHeadMap>
    {
        public AccountTransactionInventoryHeadMapMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountTransactionInventoryHeadMapIID);

            // Table & Column Mappings
            this.ToTable("AccountTransactionInventoryHeadMap", "account");
            this.Property(t => t.AccountTransactionInventoryHeadMapIID).HasColumnName("AccountTransactionInventoryHeadMapIID");
            this.Property(t => t.AccountTransactionID).HasColumnName("AccountTransactionID");
            this.Property(t => t.TransactionHeadID).HasColumnName("TransactionHeadID");

            // Relationships
            this.HasRequired(t => t.AccountTransaction)
                .WithMany(t => t.AccountTransactionInventoryHeadMaps)
                .HasForeignKey(d => d.AccountTransactionID);
            this.HasRequired(t => t.TransactionHead)
                .WithMany(t => t.AccountTransactionInventoryHeadMaps)
                .HasForeignKey(d => d.TransactionHeadID);
        }
    }
}
