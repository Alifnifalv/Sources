using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwCustomerOrderTypeMap : EntityTypeConfiguration<vwCustomerOrderType>
    {
        public vwCustomerOrderTypeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OrderID, t.OrderDate });

            // Properties
            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwCustomerOrderType");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.OrderType).HasColumnName("OrderType");
            this.Property(t => t.OrderAmount).HasColumnName("OrderAmount");
        }
    }
}
