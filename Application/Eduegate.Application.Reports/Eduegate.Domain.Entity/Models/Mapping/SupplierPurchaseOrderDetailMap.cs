using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierPurchaseOrderDetailMap : EntityTypeConfiguration<SupplierPurchaseOrderDetail>
    {
        public SupplierPurchaseOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierPurchaseOrderDetailsID);

            // Properties
            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SupplierPurchaseOrderDetails");
            this.Property(t => t.SupplierPurchaseOrderDetailsID).HasColumnName("SupplierPurchaseOrderDetailsID");
            this.Property(t => t.RefSupplierPurchaseOrderMasterID).HasColumnName("RefSupplierPurchaseOrderMasterID");
            this.Property(t => t.RefOrderItemID).HasColumnName("RefOrderItemID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.CancelQty).HasColumnName("CancelQty");
            this.Property(t => t.CostPrice).HasColumnName("CostPrice");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ProductManagerID).HasColumnName("ProductManagerID");
        }
    }
}
