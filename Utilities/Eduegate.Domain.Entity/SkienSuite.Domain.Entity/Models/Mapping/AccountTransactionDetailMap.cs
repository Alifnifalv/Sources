using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AccountTransactionDetailMap : EntityTypeConfiguration<AccountTransactionDetail>
    {
        public AccountTransactionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.AccountTransactionDetailIID);

            // Properties
            this.Property(t => t.ReferenceNumber)
                .HasMaxLength(50);

            this.Property(t => t.Remarks)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AccountTransactionDetails", "account");
            this.Property(t => t.AccountTransactionDetailIID).HasColumnName("AccountTransactionDetailIID");
            this.Property(t => t.AccountTransactionHeadID).HasColumnName("AccountTransactionHeadID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CostCenterID).HasColumnName("CostCenterID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.InvoiceAmount).HasColumnName("InvoiceAmount");
            this.Property(t => t.PaidAmount).HasColumnName("PaidAmount");
            this.Property(t => t.ReturnAmount).HasColumnName("ReturnAmount");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.PaymentDueDate).HasColumnName("PaymentDueDate");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.AccountTransactionDetails)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.AccountTransactionHead)
                .WithMany(t => t.AccountTransactionDetails)
                .HasForeignKey(d => d.AccountTransactionHeadID);

        }
    }
}
