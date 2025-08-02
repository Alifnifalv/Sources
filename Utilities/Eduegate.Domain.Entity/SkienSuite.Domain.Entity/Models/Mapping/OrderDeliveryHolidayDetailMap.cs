using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderDeliveryHolidayDetailMap : EntityTypeConfiguration<OrderDeliveryHolidayDetail>
    {
        public OrderDeliveryHolidayDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderDeliveryHolidayDetailsIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderDeliveryHolidayDetails", "distribution");
            this.Property(t => t.OrderDeliveryHolidayDetailsIID).HasColumnName("OrderDeliveryHolidayDetailsIID");
            this.Property(t => t.OrderDeliveryHolidayHeadID).HasColumnName("OrderDeliveryHolidayHeadID");
            this.Property(t => t.HolidayDate).HasColumnName("HolidayDate");

            // Relationships
            this.HasOptional(t => t.OrderDeliveryHolidayHead)
                .WithMany(t => t.OrderDeliveryHolidayDetails)
                .HasForeignKey(d => d.OrderDeliveryHolidayHeadID);

        }
    }
}
