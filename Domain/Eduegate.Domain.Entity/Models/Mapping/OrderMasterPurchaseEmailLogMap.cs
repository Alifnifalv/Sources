using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMasterPurchaseEmailLogMap : EntityTypeConfiguration<OrderMasterPurchaseEmailLog>
    {
        public OrderMasterPurchaseEmailLogMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseEmailLogID);

            // Properties
            this.Property(t => t.Action)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("OrderMasterPurchaseEmailLog");
            this.Property(t => t.PurchaseEmailLogID).HasColumnName("PurchaseEmailLogID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.Action).HasColumnName("Action");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
        }
    }
}
