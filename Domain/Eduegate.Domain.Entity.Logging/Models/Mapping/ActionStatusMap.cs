using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Logging.Models.Mapping
{
    public class ActionStatusMap : EntityTypeConfiguration<ActionStatus>
    {
        public ActionStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.ActionStatusID);

            // Properties
            this.Property(t => t.ActionStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ActionStatuses", "analytics");
            this.Property(t => t.ActionStatusID).HasColumnName("ActionStatusID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
