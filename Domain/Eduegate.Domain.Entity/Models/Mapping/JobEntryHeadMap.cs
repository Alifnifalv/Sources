using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobEntryHeadMap : EntityTypeConfiguration<JobEntryHead>
    {
        public JobEntryHeadMap()
        {
            // Primary Key
            this.HasKey(t => t.JobEntryHeadIID);

            // Properties
            this.Property(t => t.JobNumber)
                .HasMaxLength(50);
                
            this.Property(t => t.Remarks)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("JobEntryHeads", "jobs");
            this.Property(t => t.JobEntryHeadIID).HasColumnName("JobEntryHeadIID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.JobNumber).HasColumnName("JobNumber");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.JobStartDate).HasColumnName("JobStartDate");
            this.Property(t => t.JobEndDate).HasColumnName("JobEndDate");
            this.Property(t => t.ReferenceDocumentTypeID).HasColumnName("ReferenceDocumentTypeID");
            this.Property(t => t.TransactionHeadID).HasColumnName("TransactionHeadID");
            this.Property(t => t.PriorityID).HasColumnName("PriorityID");
            this.Property(t => t.BasketID).HasColumnName("BasketID");
            this.Property(t => t.JobStatusID).HasColumnName("JobStatusID");
            this.Property(t => t.JobOperationStatusID).HasColumnName("JobOperationStatusID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.ProcessStartDate).HasColumnName("ProcessStartDate");
            this.Property(t => t.ProcessEndDate).HasColumnName("ProcessEndDate");
            this.Property(t => t.VehicleID).HasColumnName("VehicleID");
            this.Property(t => t.IsCashCollected).HasColumnName("IsCashCollected");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.NoOfPacket).HasColumnName("NoOfPacket");
            this.Property(t => t.ServiceProviderId).HasColumnName("ServiceProviderId");
            this.Property(t => t.ParentJobEntryHeadId).HasColumnName("ParentJobEntryHeadId");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.OrderContactMapID).HasColumnName("OrderContactMapID");

            // Relationships
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.TransactionHeadID);
            this.HasOptional(t => t.Basket)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.BasketID);
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.BranchID);
            this.HasOptional(t => t.DocumentType)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.ReferenceDocumentTypeID);
            this.HasOptional(t => t.DocumentType1)
                .WithMany(t => t.JobEntryHeads1)
                .HasForeignKey(d => d.DocumentTypeID);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.EmployeeID);
            this.HasOptional(t => t.JobOperationStatus)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.JobOperationStatusID);
            this.HasOptional(t => t.JobStatus)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.JobStatusID);
            this.HasOptional(t => t.Priority)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.PriorityID);
            this.HasOptional(t => t.Vehicle)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.VehicleID);
            this.HasOptional(t => t.ServiceProvider)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.ServiceProviderId);
            this.HasOptional(t => t.OrderContactMap)
                .WithMany(t => t.JobEntryHeads)
                .HasForeignKey(d => d.OrderContactMapID);
        }
    }
}
