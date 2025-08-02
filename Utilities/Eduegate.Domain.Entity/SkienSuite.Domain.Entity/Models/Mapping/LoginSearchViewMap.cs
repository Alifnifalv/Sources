using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LoginSearchViewMap : EntityTypeConfiguration<LoginSearchView>
    {
        public LoginSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.LoginIID);

            // Properties
            this.Property(t => t.LoginIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LoginUserID)
                .HasMaxLength(100);

            this.Property(t => t.LoginEmailID)
                .HasMaxLength(100);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LoginSearchView", "mutual");
            this.Property(t => t.LoginIID).HasColumnName("LoginIID");
            this.Property(t => t.LoginUserID).HasColumnName("LoginUserID");
            this.Property(t => t.LoginEmailID).HasColumnName("LoginEmailID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
        }
    }
}
