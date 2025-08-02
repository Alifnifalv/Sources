using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderDeliveryHolidayHeadMap : EntityTypeConfiguration<OrderDeliveryHolidayHead>
    {
        public OrderDeliveryHolidayHeadMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderDeliveryHolidayHeadIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderDeliveryHolidayHead", "distribution");
            this.Property(t => t.OrderDeliveryHolidayHeadIID).HasColumnName("OrderDeliveryHolidayHeadIID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.WeekEndDayID1).HasColumnName("WeekEndDayID1");
            this.Property(t => t.WeekEndDayID2).HasColumnName("WeekEndDayID2");

            // Relationships
            this.HasOptional(t => t.Site)
                .WithMany(t => t.OrderDeliveryHolidayHeads)
                .HasForeignKey(d => d.SiteID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.OrderDeliveryHolidayHeads)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.OrderDeliveryHolidayHeads)
                .HasForeignKey(d => d.DeliveryTypeID);

        }
    }
}
