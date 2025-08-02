using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierPurchaseOrderMasterMap : EntityTypeConfiguration<SupplierPurchaseOrderMaster>
    {
        public SupplierPurchaseOrderMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierPurchaseOrderMasterID);

            // Properties
            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SupplierPurchaseOrderMaster");
            this.Property(t => t.SupplierPurchaseOrderMasterID).HasColumnName("SupplierPurchaseOrderMasterID");
            this.Property(t => t.RefSupplierID).HasColumnName("RefSupplierID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.EmailSend).HasColumnName("EmailSend");
            this.Property(t => t.EmailSendOn).HasColumnName("EmailSendOn");
            this.Property(t => t.CancelEmailSend).HasColumnName("CancelEmailSend");
            this.Property(t => t.CancelEmailSendOn).HasColumnName("CancelEmailSendOn");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
