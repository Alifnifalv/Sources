using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WalletTransactionMasterMap : EntityTypeConfiguration<WalletTransactionMaster>
    {
        public WalletTransactionMasterMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TransactionRelationId, t.LanguageID });

            // Properties
            this.Property(t => t.TransactionRelationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LanguageID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("WalletTransactionMaster", "wlt");
            this.Property(t => t.TransactionRelationId).HasColumnName("TransactionRelationId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.LanguageID).HasColumnName("LanguageID");
        }

    }
}
