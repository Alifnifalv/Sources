using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserViewMap : EntityTypeConfiguration<UserView>
    {
        public UserViewMap()
        {
            // Primary Key
            this.HasKey(t => t.UserViewIID);

            // Properties
            this.Property(t => t.UserViewName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UserViews", "setting");
            this.Property(t => t.UserViewIID).HasColumnName("UserViewIID");
            this.Property(t => t.UserViewName).HasColumnName("UserViewName");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.UserViews)
                .HasForeignKey(d => d.LoginID);
            this.HasOptional(t => t.View)
                .WithMany(t => t.UserViews)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
