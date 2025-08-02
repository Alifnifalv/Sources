using Eduegate.Domain.Entity.Models.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping.Inventory
{
    public class TaxMap : EntityTypeConfiguration<Tax>
    {
        public TaxMap()
        {
            // Primary Key
            this.HasKey(t => t.TaxID);

            // Properties
            this.Property(t => t.TaxID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TaxName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Taxes", "inventory");
            this.Property(t => t.TaxID).HasColumnName("TaxID");
            this.Property(t => t.TaxName).HasColumnName("TaxName");
            this.Property(t => t.TaxTypeID).HasColumnName("TaxTypeID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.Percentage).HasColumnName("Percentage");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.TaxStatusID).HasColumnName("TaxStatusID");

            // Relationships
            this.HasOptional(t => t.Account)
                .WithMany(t => t.Taxes)
                .HasForeignKey(d => d.AccountID);
            this.HasOptional(t => t.TaxStatus)
                .WithMany(t => t.Taxes)
                .HasForeignKey(d => d.TaxStatusID);
            this.HasOptional(t => t.TaxType)
                .WithMany(t => t.Taxes)
                .HasForeignKey(d => d.TaxTypeID);

        }
    }
}
