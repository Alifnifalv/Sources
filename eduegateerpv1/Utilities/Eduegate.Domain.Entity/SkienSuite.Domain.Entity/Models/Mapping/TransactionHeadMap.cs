using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TransactionHeadMap : EntityTypeConfiguration<TransactionHead>
    {
        public TransactionHeadMap()
        {
            // Primary Key
            this.HasKey(t => t.HeadIID);

            // Properties
            this.Property(t => t.TransactionNo)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Reference)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransactionHead", "inventory");
            this.Property(t => t.HeadIID).HasColumnName("HeadIID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.ToBranchID).HasColumnName("ToBranchID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.TransactionNo).HasColumnName("TransactionNo");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.DiscountAmount).HasColumnName("DiscountAmount");
            this.Property(t => t.DiscountPercentage).HasColumnName("DiscountPercentage");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");
            this.Property(t => t.TransactionStatusID).HasColumnName("TransactionStatusID");
            this.Property(t => t.EntitlementID).HasColumnName("EntitlementID");
            this.Property(t => t.ReferenceHeadID).HasColumnName("ReferenceHeadID");
            this.Property(t => t.JobEntryHeadID).HasColumnName("JobEntryHeadID");
            this.Property(t => t.DeliveryMethodID).HasColumnName("DeliveryMethodID");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.IsShipment).HasColumnName("IsShipment");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.DeliveryCharge).HasColumnName("DeliveryCharge");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.JobStatusID).HasColumnName("JobStatusID");
            this.Property(t => t.DocumentStatusID).HasColumnName("DocumentStatusID");
            this.Property(t => t.TransactionRole).HasColumnName("TransactionRole");
            this.Property(t => t.Reference).HasColumnName("Reference");
            this.Property(t => t.DocumentCancelledDate).HasColumnName("DocumentCancelledDate");
            this.Property(t => t.ReceivingMethodID).HasColumnName("ReceivingMethodID");
            this.Property(t => t.ReturnMethodID).HasColumnName("ReturnMethodID");

            // Relationships
            this.HasOptional(t => t.Role)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.TransactionRole);
            this.HasOptional(t => t.DeliveryType)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.DeliveryMethodID);
            this.HasOptional(t => t.ReceivingMethod)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.ReceivingMethodID);
            this.HasOptional(t => t.ReturnMethod)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.ReturnMethodID);
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.DeliveryTypeID);
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.Branch1)
                .WithMany(t => t.TransactionHeads1)
                .HasForeignKey(d => d.ToBranchID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Currency)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.CurrencyID);
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.DocumentReferenceStatusMap)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.DocumentStatusID);
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.DocumentTypeID);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.EmployeeID);
            this.HasOptional(t => t.EntityTypeEntitlement)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.EntitlementID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.SupplierID);
            this.HasOptional(t => t.TransactionHead2)
                .WithMany(t => t.TransactionHead1)
                .HasForeignKey(d => d.ReferenceHeadID);
            this.HasOptional(t => t.TransactionStatus)
                .WithMany(t => t.TransactionHeads)
                .HasForeignKey(d => d.TransactionStatusID);

        }
    }
}
