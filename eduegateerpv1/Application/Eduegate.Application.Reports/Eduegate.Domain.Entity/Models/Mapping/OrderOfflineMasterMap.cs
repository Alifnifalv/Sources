using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderOfflineMasterMap : EntityTypeConfiguration<OrderOfflineMaster>
    {
        public OrderOfflineMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderOfflineID);

            // Properties
            this.Property(t => t.OfflineInvoiceNo)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.OrderOfflineStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("OrderOfflineMaster");
            this.Property(t => t.OrderOfflineID).HasColumnName("OrderOfflineID");
            this.Property(t => t.OfflineInvoiceNo).HasColumnName("OfflineInvoiceNo");
            this.Property(t => t.OfflineDeliveryMethod).HasColumnName("OfflineDeliveryMethod");
            this.Property(t => t.RefOfflineCustomerID).HasColumnName("RefOfflineCustomerID");
            this.Property(t => t.OrderOfflineStatus).HasColumnName("OrderOfflineStatus");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedDatetime).HasColumnName("CreatedDatetime");
            this.Property(t => t.UpdatedByID).HasColumnName("UpdatedByID");
            this.Property(t => t.UpdatedDateTime).HasColumnName("UpdatedDateTime");
        }
    }
}
