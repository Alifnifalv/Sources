using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.PermissionID);

            // Properties
            this.Property(t => t.PermissionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PermissionName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Permissions", "admin");
            this.Property(t => t.PermissionID).HasColumnName("PermissionID");
            this.Property(t => t.PermissionName).HasColumnName("PermissionName");
        }
    }
}
