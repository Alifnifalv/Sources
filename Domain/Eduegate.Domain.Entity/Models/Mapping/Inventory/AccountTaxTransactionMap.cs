using Eduegate.Domain.Entity.Models.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping.Inventory
{
    public class AccountTaxTransactionMap : EntityTypeConfiguration<AccountTaxTransaction>
    {
        public AccountTaxTransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.TaxTransactionIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AccountTaxTransactions", "account");
            this.Property(t => t.TaxTransactionIID).HasColumnName("TaxTransactionIID");
            this.Property(t => t.HeadID).HasColumnName("HeadID");
            this.Property(t => t.TaxTemplateItemID).HasColumnName("TaxTemplateItemID");
            this.Property(t => t.TaxTemplateID).HasColumnName("TaxTemplateID");
            this.Property(t => t.TaxTypeID).HasColumnName("TaxTypeID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.AccoundID).HasColumnName("AccoundID");
            this.Property(t => t.Percentage).HasColumnName("Percentage");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.AccountTaxTransactions)
                .HasForeignKey(d => d.AccoundID);
            this.HasOptional(t => t.TaxTemplateItem)
                .WithMany(t => t.AccountTaxTransactions)
                .HasForeignKey(d => d.TaxTemplateItemID);
            this.HasOptional(t => t.TaxTemplate)
                .WithMany(t => t.AccountTaxTransactions)
                .HasForeignKey(d => d.TaxTemplateID);
            this.HasOptional(t => t.TaxType)
                .WithMany(t => t.AccountTaxTransactions)
                .HasForeignKey(d => d.TaxTypeID);
            this.HasOptional(t => t.AccountTransactionHead)
                .WithMany(t => t.AccountTaxTransactions)
                .HasForeignKey(d => d.HeadID);
        }
    }
}
