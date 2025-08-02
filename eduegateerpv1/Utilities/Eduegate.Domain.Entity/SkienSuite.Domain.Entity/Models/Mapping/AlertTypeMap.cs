using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AlertTypeMap : EntityTypeConfiguration<AlertType>
    {
        public AlertTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.AlertTypeID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("AlertTypes", "notification");
            this.Property(t => t.AlertTypeID).HasColumnName("AlertTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
