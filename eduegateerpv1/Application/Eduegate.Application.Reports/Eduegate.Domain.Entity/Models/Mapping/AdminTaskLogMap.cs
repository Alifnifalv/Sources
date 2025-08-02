using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AdminTaskLogMap : EntityTypeConfiguration<AdminTaskLog>
    {
        public AdminTaskLogMap()
        {
            // Primary Key
            this.HasKey(t => t.TaskID);

            // Properties
            this.Property(t => t.UpdateType)
                .HasMaxLength(10);

            this.Property(t => t.OldValue)
                .HasMaxLength(100);

            this.Property(t => t.NewValue)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("AdminTaskLog");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UpdateType).HasColumnName("UpdateType");
            this.Property(t => t.OldValue).HasColumnName("OldValue");
            this.Property(t => t.NewValue).HasColumnName("NewValue");
            this.Property(t => t.UpdateOn).HasColumnName("UpdateOn");
        }
    }
}
