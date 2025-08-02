using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AccountTransactionMap : EntityTypeConfiguration<AccountTransaction>
    {
        public AccountTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionIID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AccountTransactions", "account");
            this.Property(t => t.TransactionIID).HasColumnName("TransactionIID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.TransactionNumber).HasColumnName("TransactionNumber");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.DiscountAmount).HasColumnName("DiscountAmount");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.InclusiveTaxAmount).HasColumnName("InclusiveTaxAmount");
            this.Property(t => t.ExclusiveTaxAmount).HasColumnName("ExclusiveTaxAmount");
            this.Property(t => t.DebitOrCredit).HasColumnName("DebitOrCredit");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.CostCenterID).HasColumnName("CostCenterID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.AccountTransactions)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.CostCenter)
                .WithMany(t => t.AccountTransactions)
                .HasForeignKey(d => d.CostCenterID);
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.AccountTransactions)
                .HasForeignKey(d => d.DocumentTypeID);
        }
    }
}
