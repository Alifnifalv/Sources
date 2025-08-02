using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMasterAddressLogMap : EntityTypeConfiguration<OrderMasterAddressLog>
    {
        public OrderMasterAddressLogMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderMasterAddressLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderMasterAddressLog");
            this.Property(t => t.OrderMasterAddressLogID).HasColumnName("OrderMasterAddressLogID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
