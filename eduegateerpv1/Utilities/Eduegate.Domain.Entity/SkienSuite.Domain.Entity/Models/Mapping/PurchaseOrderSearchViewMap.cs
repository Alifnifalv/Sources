using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PurchaseOrderSearchViewMap : EntityTypeConfiguration<PurchaseOrderSearchView>
    {
        public PurchaseOrderSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.HeadIID, t.RowCategory, t.PartNumber, t.Country, t.CommentCounts });

            // Properties
            this.Property(t => t.HeadIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TransactionNo)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Supplier)
                .HasMaxLength(767);

            this.Property(t => t.RowCategory)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.PartNumber)
                .IsRequired();

            this.Property(t => t.Country)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.DisplayCode)
                .HasMaxLength(20);

            this.Property(t => t.CommentCounts)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DeliveryTypeName)
                .HasMaxLength(100);

            this.Property(t => t.ReceivingMethodName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("PurchaseOrderSearchView", "inventory");
            this.Property(t => t.HeadIID).HasColumnName("HeadIID");
            this.Property(t => t.TransactionNo).HasColumnName("TransactionNo");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Supplier).HasColumnName("Supplier");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");
            this.Property(t => t.RowCategory).HasColumnName("RowCategory");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.PIStatus).HasColumnName("PIStatus");
            this.Property(t => t.PartNumber).HasColumnName("PartNumber");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.ShoppingCartIID).HasColumnName("ShoppingCartIID");
            this.Property(t => t.EntitlementName).HasColumnName("EntitlementName");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.ProductOwner).HasColumnName("ProductOwner");
            this.Property(t => t.JobStatus).HasColumnName("JobStatus");
            this.Property(t => t.DisplayCode).HasColumnName("DisplayCode");
            this.Property(t => t.CommentCounts).HasColumnName("CommentCounts");
            this.Property(t => t.DeliveryTypeName).HasColumnName("DeliveryTypeName");
            this.Property(t => t.ReceivingMethodName).HasColumnName("ReceivingMethodName");
        }
    }
}
