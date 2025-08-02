using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderLogCodMap : EntityTypeConfiguration<OrderLogCod>
    {
        public OrderLogCodMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderLogCod");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.RefAdminID).HasColumnName("RefAdminID");
            this.Property(t => t.LogOn).HasColumnName("LogOn");
        }
    }
}
