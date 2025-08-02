using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDeliveryDaysMasterMap : EntityTypeConfiguration<ProductDeliveryDaysMaster>
    {
        public ProductDeliveryDaysMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryDaysID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductDeliveryDaysMaster");
            this.Property(t => t.DeliveryDaysID).HasColumnName("DeliveryDaysID");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
        }
    }
}
