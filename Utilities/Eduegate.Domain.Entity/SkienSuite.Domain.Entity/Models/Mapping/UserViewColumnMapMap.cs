using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserViewColumnMapMap : EntityTypeConfiguration<UserViewColumnMap>
    {
        public UserViewColumnMapMap()
        {
            // Primary Key
            this.HasKey(t => t.UserViewColumnMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UserViewColumnMaps", "setting");
            this.Property(t => t.UserViewColumnMapIID).HasColumnName("UserViewColumnMapIID");
            this.Property(t => t.UserViewID).HasColumnName("UserViewID");
            this.Property(t => t.ViewColumnID).HasColumnName("ViewColumnID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.UserView)
                .WithMany(t => t.UserViewColumnMaps)
                .HasForeignKey(d => d.UserViewID);

        }
    }
}
