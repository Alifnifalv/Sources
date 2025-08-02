using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ReceivableMap : EntityTypeConfiguration<Receivable>
    {
        public ReceivableMap()
        {
            // Primary Key
            this.HasKey(t => t.ReceivableIID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Receivables", "account");
            this.Property(t => t.ReceivableIID).HasColumnName("ReceivableIID");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.SerialNumber).HasColumnName("SerialNumber");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ReferenceReceivablesID).HasColumnName("ReferenceReceivablesID");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.PaidAmount).HasColumnName("PaidAmount");
            this.Property(t => t.AccountPostingDate).HasColumnName("AccountPostingDate");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.TransactionStatusID).HasColumnName("TransactionStatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.Receivables)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.Currency)
                .WithMany(t => t.Receivables)
                .HasForeignKey(d => d.CurrencyID);
            this.HasOptional(t => t.DocumentStatus)
                .WithMany(t => t.Receivables)
                .HasForeignKey(d => d.DocumentStatusID);
            this.HasOptional(t => t.Receivable1)
                .WithMany(t => t.Receivables1)
                .HasForeignKey(d => d.ReferenceReceivablesID);
            this.HasOptional(t => t.TransactionStatus)
                .WithMany(t => t.Receivables)
                .HasForeignKey(d => d.TransactionStatusID);

        }
    }
}
