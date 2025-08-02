using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AccountTransactionHeadMap : EntityTypeConfiguration<AccountTransactionHead>
    {
        public AccountTransactionHeadMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountTransactionHeadIID);

            // Properties
            this.Property(t => t.TransactionNumber)
                .HasMaxLength(50);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AccountTransactionHeads", "account");
            this.Property(t => t.AccountTransactionHeadIID).HasColumnName("AccountTransactionHeadIID");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.TransactionNumber).HasColumnName("TransactionNumber");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.PaymentModeID).HasColumnName("PaymentModeID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.IsPrePaid).HasColumnName("IsPrePaid");
            this.Property(t => t.AdvanceAmount).HasColumnName("AdvanceAmount");
            this.Property(t => t.CostCenterID).HasColumnName("CostCenterID");
            this.Property(t => t.AmountPaid).HasColumnName("AmountPaid");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");
            this.Property(t => t.TransactionStatusID).HasColumnName("TransactionStatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.AccountTransactionHeads)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.DocumentStatus)
                .WithMany(t => t.AccountTransactionHeads)
                .HasForeignKey(d => d.DocumentStatusID);
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.AccountTransactionHeads)
                .HasForeignKey(d => d.DocumentTypeID);
            this.HasOptional(t => t.TransactionStatus)
                .WithMany(t => t.AccountTransactionHeads)
                .HasForeignKey(d => d.TransactionStatusID);

        }
    }
}
