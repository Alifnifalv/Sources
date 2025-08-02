using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Logging.Models.Mapping
{
    public class ActionTypeMap : EntityTypeConfiguration<ActionType>
    {
        public ActionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ActionTypeID);

            // Properties
            this.Property(t => t.ActionTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ActionTypeName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ActionTypes", "analytics");
            this.Property(t => t.ActionTypeID).HasColumnName("ActionTypeID");
            this.Property(t => t.ActionTypeName).HasColumnName("ActionTypeName");
        }
    }
}
