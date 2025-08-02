using System.ComponentModel.DataAnnotations.Schema;
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

            this.Property(t => t.ExternalReference1)
               .HasMaxLength(50);

            this.Property(t => t.ExternalReference2)
               .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.ChequeNumber)
                .HasMaxLength(30);

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
            this.Property(t => t.Reference).HasColumnName("Reference");
            this.Property(t => t.IsPrePaid).HasColumnName("IsPrePaid");
            this.Property(t => t.AdvanceAmount).HasColumnName("AdvanceAmount");
            this.Property(t => t.CostCenterID).HasColumnName("CostCenterID");
            this.Property(t => t.AmountPaid).HasColumnName("AmountPaid");
            this.Property(t => t.TaxAmount).HasColumnName("TaxAmount");
            this.Property(t => t.DiscountAmount).HasColumnName("DiscountAmount");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");
            this.Property(t => t.TransactionStatusID).HasColumnName("TransactionStatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.ReceiptsID).HasColumnName("ReceiptsID");
            this.Property(t => t.PaymentsID).HasColumnName("PaymentsID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.ExternalReference1).HasColumnName("ExternalReference1");
            this.Property(t => t.ExternalReference2).HasColumnName("ExternalReference2");
            this.Property(t => t.ChequeNumber).HasColumnName("ChequeNumber");
            this.Property(t => t.ChequeDate).HasColumnName("ChequeDate");
            //this.Property(t => t.ReferenceRate).HasColumnName("ReferenceRate");
            //this.Property(t => t.ReferenceQuatity).HasColumnName("ReferenceQuatity");
        }
    }
}
