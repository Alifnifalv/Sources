using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierPurchaseOrderMap : EntityTypeConfiguration<SupplierPurchaseOrder>
    {
        public SupplierPurchaseOrderMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierPurchaseOrderID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SupplierPurchaseOrder");
            this.Property(t => t.SupplierPurchaseOrderID).HasColumnName("SupplierPurchaseOrderID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.RefSupplierID).HasColumnName("RefSupplierID");
            this.Property(t => t.RefProductManagerID).HasColumnName("RefProductManagerID");
            this.Property(t => t.OrderQty).HasColumnName("OrderQty");
            this.Property(t => t.CancelReturnQty).HasColumnName("CancelReturnQty");
            this.Property(t => t.CostPrice).HasColumnName("CostPrice");
            this.Property(t => t.SellingPrice).HasColumnName("SellingPrice");
            this.Property(t => t.EmailSend).HasColumnName("EmailSend");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
