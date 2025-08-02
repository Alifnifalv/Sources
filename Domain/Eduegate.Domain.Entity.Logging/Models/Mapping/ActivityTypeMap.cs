using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Logging.Models.Mapping
{
    public class ActivityTypeMap : EntityTypeConfiguration<ActivityType>
    {
        public ActivityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ActivityTypeID);

            // Properties
            this.Property(t => t.ActivityTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActivityTypeName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ActivityTypes", "analytics");
            this.Property(t => t.ActivityTypeID).HasColumnName("ActivityTypeID");
            this.Property(t => t.ActivityTypeName).HasColumnName("ActivityTypeName");
        }
    }
}
