using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderAttributeMap : EntityTypeConfiguration<OrderAttribute>
    {
        public OrderAttributeMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderAttributeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderAttribute");
            this.Property(t => t.OrderAttributeID).HasColumnName("OrderAttributeID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefOrderSizeID).HasColumnName("RefOrderSizeID");
        }
    }
}
