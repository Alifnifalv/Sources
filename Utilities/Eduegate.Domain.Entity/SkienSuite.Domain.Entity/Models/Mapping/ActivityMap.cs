using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ActivityMap : EntityTypeConfiguration<Activity>
    {
        public ActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.ActivitiyIID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("Activities", "analytics");
            this.Property(t => t.ActivitiyIID).HasColumnName("ActivitiyIID");
            this.Property(t => t.ActivityTypeID).HasColumnName("ActivityTypeID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Created).HasColumnName("Created");

            // Relationships
            this.HasOptional(t => t.ActivityType)
                .WithMany(t => t.Activities)
                .HasForeignKey(d => d.ActivityTypeID);

        }
    }
}
