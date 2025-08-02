using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LoginRoleMapMap : EntityTypeConfiguration<LoginRoleMap>
    {
        public LoginRoleMapMap()
        {
            // Primary Key
            this.HasKey(t => t.LoginRoleMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("LoginRoleMaps", "admin");
            this.Property(t => t.LoginRoleMapIID).HasColumnName("LoginRoleMapIID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.LoginRoleMaps)
                .HasForeignKey(d => d.LoginID);
            
        }
    }
}
