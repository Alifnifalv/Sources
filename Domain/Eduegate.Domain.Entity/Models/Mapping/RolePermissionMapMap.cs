using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class RolePermissionMapMap : EntityTypeConfiguration<RolePermissionMap>
    {
        public RolePermissionMapMap()
        {
            // Primary Key
            this.HasKey(t => t.RolePermissionMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("RolePermissionMaps", "admin");
            this.Property(t => t.RolePermissionMapIID).HasColumnName("RolePermissionMapIID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.PermissionID).HasColumnName("PermissionID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Permission)
                .WithMany(t => t.RolePermissionMaps)
                .HasForeignKey(d => d.PermissionID);
            this.HasOptional(t => t.Role)
                .WithMany(t => t.RolePermissionMaps)
                .HasForeignKey(d => d.RoleID);

        }
    }
}
