using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class RoleCultureDataMap : EntityTypeConfiguration<RoleCultureData>
    {
        public RoleCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.CultureID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RoleName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("RoleCultureDatas", "admin");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.RoleCultureDatas)
                .HasForeignKey(d => d.CultureID);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RoleCultureDatas)
                .HasForeignKey(d => d.RoleID);

        }
    }
}
