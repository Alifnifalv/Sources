using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class JobCardMasterMap : EntityTypeConfiguration<JobCardMaster>
    {
        public JobCardMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.JobCardMasterID);

            // Properties
            this.Property(t => t.SystemType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SrNo)
                .HasMaxLength(50);

            this.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Telephone)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.InvoiceNo)
                .HasMaxLength(30);

            this.Property(t => t.Accessories)
                .HasMaxLength(15);

            this.Property(t => t.Problems)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.Solution)
                .HasMaxLength(300);

            this.Property(t => t.JobCardStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("JobCardMaster");
            this.Property(t => t.JobCardMasterID).HasColumnName("JobCardMasterID");
            this.Property(t => t.SystemType).HasColumnName("SystemType");
            this.Property(t => t.SrNo).HasColumnName("SrNo");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.ReceivingDate).HasColumnName("ReceivingDate");
            this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");
            this.Property(t => t.Warranty).HasColumnName("Warranty");
            this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo");
            this.Property(t => t.InvoiceDate).HasColumnName("InvoiceDate");
            this.Property(t => t.Accessories).HasColumnName("Accessories");
            this.Property(t => t.Problems).HasColumnName("Problems");
            this.Property(t => t.WithData).HasColumnName("WithData");
            this.Property(t => t.Solution).HasColumnName("Solution");
            this.Property(t => t.EstimatedCharges).HasColumnName("EstimatedCharges");
            this.Property(t => t.JobCardStatus).HasColumnName("JobCardStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
