using Eduegate.Domain.Entity.Models.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping.Inventory
{
    public class TaxTemplateItemMap : EntityTypeConfiguration<TaxTemplateItem>
    {
        public TaxTemplateItemMap()
        {
            // Primary Key
            this.HasKey(t => t.TaxTemplateItemID);

            // Properties
            this.Property(t => t.TaxTemplateItemID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("TaxTemplateItems", "inventory");
            this.Property(t => t.TaxTemplateItemID).HasColumnName("TaxTemplateItemID");
            this.Property(t => t.TaxTemplateID).HasColumnName("TaxTemplateID");
            this.Property(t => t.TaxTypeID).HasColumnName("TaxTypeID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Percentage).HasColumnName("Percentage");
            this.Property(t => t.HasTaxInclusive).HasColumnName("HasTaxInclusive");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.TaxTemplateItems)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.TaxTemplate)
                .WithMany(t => t.TaxTemplateItems)
                .HasForeignKey(d => d.TaxTemplateID);
            this.HasOptional(t => t.TaxType)
                .WithMany(t => t.TaxTemplateItems)
                .HasForeignKey(d => d.TaxTypeID);

        }
    }
}
