using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PurchaseInvoiceSearchViewMap : EntityTypeConfiguration<PurchaseInvoiceSearchView>
    {
        public PurchaseInvoiceSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.HeadIID, t.RowCategory, t.PartNumber, t.CommentCounts });

            // Properties
            this.Property(t => t.HeadIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TransactionNo)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Supplier)
                .HasMaxLength(767);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.RowCategory)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Reference)
                .HasMaxLength(50);

            this.Property(t => t.PartNumber)
                .IsRequired();

            this.Property(t => t.DisplayCode)
                .HasMaxLength(20);

            this.Property(t => t.CommentCounts)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PurchaseInvoiceSearchView", "inventory");
            this.Property(t => t.HeadIID).HasColumnName("HeadIID");
            this.Property(t => t.TransactionNo).HasColumnName("TransactionNo");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Supplier).HasColumnName("Supplier");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.RowCategory).HasColumnName("RowCategory");
            this.Property(t => t.EntitlementName).HasColumnName("EntitlementName");
            this.Property(t => t.ShoppingCartIID).HasColumnName("ShoppingCartIID");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.PurchaseOrder).HasColumnName("PurchaseOrder");
            this.Property(t => t.Reference).HasColumnName("Reference");
            this.Property(t => t.ProductOwner).HasColumnName("ProductOwner");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.PartNumber).HasColumnName("PartNumber");
            this.Property(t => t.DisplayCode).HasColumnName("DisplayCode");
            this.Property(t => t.CommentCounts).HasColumnName("CommentCounts");
        }
    }
}
