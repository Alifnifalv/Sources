using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Logging.Models.Mapping
{
    public class ActivityMap : EntityTypeConfiguration<Activity>
    {
        public ActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.ActivitiyIID);

            // Properties
            this.Property(t => t.ReferenceID)
                .HasMaxLength(500);

            this.Property(t => t.Description)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("Activities", "analytics");
            this.Property(t => t.ActivitiyIID).HasColumnName("ActivitiyIID");
            this.Property(t => t.ActivityTypeID).HasColumnName("ActivityTypeID");
            this.Property(t => t.ActionTypeID).HasColumnName("ActionTypeID");
            this.Property(t => t.ActionStatusID).HasColumnName("ActionStatusID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Created).HasColumnName("Created");

            // Relationships
            this.HasRequired(t => t.ActionStatus)
                .WithMany(t => t.Activities)
                .HasForeignKey(d => d.ActionStatusID);
            this.HasOptional(t => t.ActionType)
                .WithMany(t => t.Activities)
                .HasForeignKey(d => d.ActionTypeID);
            this.HasOptional(t => t.ActivityType)
                .WithMany(t => t.Activities)
                .HasForeignKey(d => d.ActivityTypeID);

        }
    }
}
