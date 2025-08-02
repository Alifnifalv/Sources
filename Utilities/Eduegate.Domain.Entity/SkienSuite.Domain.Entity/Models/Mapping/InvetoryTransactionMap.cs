using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class InvetoryTransactionMap : EntityTypeConfiguration<InvetoryTransaction>
    {
        public InvetoryTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.InventoryTransactionIID);

            // Properties
            this.Property(t => t.TransactionNo)
                .HasMaxLength(30);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("InvetoryTransactions", "inventory");
            this.Property(t => t.InventoryTransactionIID).HasColumnName("InventoryTransactionIID");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.TransactionNo).HasColumnName("TransactionNo");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            this.Property(t => t.Cost).HasColumnName("Cost");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Rate).HasColumnName("Rate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.LinkDocumentID).HasColumnName("LinkDocumentID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.HeadID).HasColumnName("HeadID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.InvetoryTransactions)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.Unit)
                .WithMany(t => t.InvetoryTransactions)
                .HasForeignKey(d => d.UnitID);
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.InvetoryTransactions)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.InvetoryTransactions)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Currency)
                .WithMany(t => t.InvetoryTransactions)
                .HasForeignKey(d => d.CurrencyID);
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.InvetoryTransactions)
                .HasForeignKey(d => d.DocumentTypeID);
            this.HasOptional(t => t.InvetoryTransaction1)
                .WithMany(t => t.InvetoryTransactions1)
                .HasForeignKey(d => d.LinkDocumentID);
            this.HasRequired(t => t.InvetoryTransaction2)
                .WithOptional(t => t.InvetoryTransactions11);
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.InvetoryTransactions)
                .HasForeignKey(d => d.HeadID);

        }
    }
}
