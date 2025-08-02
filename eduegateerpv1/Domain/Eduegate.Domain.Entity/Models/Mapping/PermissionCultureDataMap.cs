using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PermissionCultureDataMap : EntityTypeConfiguration<PermissionCultureData>
    {
        public PermissionCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PermissionID, t.CultureID });

            // Properties
            this.Property(t => t.PermissionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PermissionName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PermissionCultureDatas", "admin");
            this.Property(t => t.PermissionID).HasColumnName("PermissionID");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.PermissionName).HasColumnName("PermissionName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.PermissionCultureDatas)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.Permission)
                .WithMany(t => t.PermissionCultureDatas)
                .HasForeignKey(d => d.PermissionID);

        }
    }
}
