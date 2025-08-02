using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PoTrackingMasterMap : EntityTypeConfiguration<PoTrackingMaster>
    {
        public PoTrackingMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.PoTrackingMasterID);

            // Properties
            this.Property(t => t.PoNumber)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.SupplierName)
                .HasMaxLength(50);

            this.Property(t => t.Details)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.PoTrackingStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.InvoiceNo)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("PoTrackingMaster");
            this.Property(t => t.PoTrackingMasterID).HasColumnName("PoTrackingMasterID");
            this.Property(t => t.PoNumber).HasColumnName("PoNumber");
            this.Property(t => t.SupplierName).HasColumnName("SupplierName");
            this.Property(t => t.PoValue).HasColumnName("PoValue");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.RefPoTrackingWorkflowNo).HasColumnName("RefPoTrackingWorkflowNo");
            this.Property(t => t.PoTrackingStatus).HasColumnName("PoTrackingStatus");
            this.Property(t => t.RefPoTrackingWorkflowID).HasColumnName("RefPoTrackingWorkflowID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.ProductManagerID).HasColumnName("ProductManagerID");
            this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo");
            this.Property(t => t.RefCurrencyID).HasColumnName("RefCurrencyID");

            // Relationships
            this.HasOptional(t => t.CurrencyMaster)
                .WithMany(t => t.PoTrackingMasters)
                .HasForeignKey(d => d.RefCurrencyID);

        }
    }
}
