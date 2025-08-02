using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PublicHolidayMap : EntityTypeConfiguration<PublicHoliday>
    {
        public PublicHolidayMap()
        {
            // Primary Key
            this.HasKey(t => t.HolidayDate);

            // Properties
            // Table & Column Mappings
            this.ToTable("PublicHolidays");
            this.Property(t => t.HolidayDate).HasColumnName("HolidayDate");
        }
    }
}
