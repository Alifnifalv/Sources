using System.ComponentModel.DataAnnotations.Schema;
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
                .HasMaxLength(2000);

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
            //this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.TaxTemplateID).HasColumnName("TaxTemplateID");
            this.Property(t => t.TaxPercentage).HasColumnName("TaxPercentage");
            this.Property(t => t.ReferencePaymentID).HasColumnName("ReferencePaymentID");
            this.Property(t => t.ReferenceReceiptID).HasColumnName("ReferenceReceiptID");
            this.Property(t => t.ExternalReference1).HasColumnName("ExternalReference1");
            this.Property(t => t.ExternalReference2).HasColumnName("ExternalReference2");
            this.Property(t => t.ExternalReference3).HasColumnName("ExternalReference3");
            this.Property(t => t.ReferenceQuantity).HasColumnName("ReferenceQuantity");
            this.Property(t => t.ReferenceRate).HasColumnName("ReferenceRate");
            this.Property(t => t.TaxAmount).HasColumnName("TaxAmount");
            this.Property(t => t.DiscountAmount).HasColumnName("DiscountAmount");
            this.Property(t => t.SubLedgerID).HasColumnName("SubLedgerID");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.AccountTransactionDetails)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.AccountTransactionHead)
                .WithMany(t => t.AccountTransactionDetails)
                .HasForeignKey(d => d.AccountTransactionHeadID);
            this.HasOptional(t => t.ProductSKUMap)
               .WithMany(t => t.AccountTransactionDetails)
               .HasForeignKey(d => d.ProductSKUId);
            this.HasOptional(t => t.Accounts_SubLedger)
               .WithMany(t => t.AccountTransactionDetails)
               .HasForeignKey(d => d.SubLedgerID);

            this.HasOptional(t => t.Department)
              .WithMany(t => t.AccountTransactionDetails)
              .HasForeignKey(d => d.DepartmentID);
        }
    }
}
