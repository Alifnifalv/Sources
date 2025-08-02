using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ExpressDeliverySlotsHolidayMap : EntityTypeConfiguration<ExpressDeliverySlotsHoliday>
    {
        public ExpressDeliverySlotsHolidayMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TimeFrom, t.TimeTo });

            // Properties
            this.Property(t => t.TimeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("ExpressDeliverySlotsHoliday");
            this.Property(t => t.TimeID).HasColumnName("TimeID");
            this.Property(t => t.TimeFrom).HasColumnName("TimeFrom");
            this.Property(t => t.TimeTo).HasColumnName("TimeTo");
        }
    }
}
