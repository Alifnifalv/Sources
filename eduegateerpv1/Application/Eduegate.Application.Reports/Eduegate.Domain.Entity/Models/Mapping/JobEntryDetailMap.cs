using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobEntryDetailMap : EntityTypeConfiguration<JobEntryDetail>
    {
        public JobEntryDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.JobEntryDetailIID);

            // Properties
            this.Property(t => t.ValidatedPartNo)
                .HasMaxLength(50);

            this.Property(t => t.ValidationBarCode)
                .HasMaxLength(50);

            this.Property(t => t.Remarks)
                .HasMaxLength(1000);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.AWBNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("JobEntryDetails", "jobs");
            this.Property(t => t.JobEntryDetailIID).HasColumnName("JobEntryDetailIID");
            this.Property(t => t.JobEntryHeadID).HasColumnName("JobEntryHeadID");
            this.Property(t => t.ProductSKUID).HasColumnName("ProductSKUID");
            this.Property(t => t.ParentJobEntryHeadID).HasColumnName("ParentJobEntryHeadID");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.IsQuantiyVerified).HasColumnName("IsQuantiyVerified");
            this.Property(t => t.IsBarCodeVerified).HasColumnName("IsBarCodeVerified");
            this.Property(t => t.IsLocationVerified).HasColumnName("IsLocationVerified");
            this.Property(t => t.JobStatusID).HasColumnName("JobStatusID");
            this.Property(t => t.ValidatedQuantity).HasColumnName("ValidatedQuantity");
            this.Property(t => t.ValidatedLocationID).HasColumnName("ValidatedLocationID");
            this.Property(t => t.ValidatedPartNo).HasColumnName("ValidatedPartNo");
            this.Property(t => t.ValidationBarCode).HasColumnName("ValidationBarCode");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.AWBNo).HasColumnName("AWBNo");

            // Relationships
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.JobEntryDetails)
                .HasForeignKey(d => d.ProductSKUID);
            this.HasOptional(t => t.JobEntryHead)
                .WithMany(t => t.JobEntryDetails)
                .HasForeignKey(d => d.JobEntryHeadID);
            this.HasOptional(t => t.JobEntryHead1)
                .WithMany(t => t.JobEntryDetails1)
                .HasForeignKey(d => d.ParentJobEntryHeadID);
            this.HasOptional(t => t.JobStatus)
                .WithMany(t => t.JobEntryDetails)
                .HasForeignKey(d => d.JobStatusID);

        }
    }
}
