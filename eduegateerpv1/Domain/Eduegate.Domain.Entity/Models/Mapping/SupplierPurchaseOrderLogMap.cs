using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierPurchaseOrderLogMap : EntityTypeConfiguration<SupplierPurchaseOrderLog>
    {
        public SupplierPurchaseOrderLogMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierPurchaseOrderLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("SupplierPurchaseOrderLog");
            this.Property(t => t.SupplierPurchaseOrderLogID).HasColumnName("SupplierPurchaseOrderLogID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
