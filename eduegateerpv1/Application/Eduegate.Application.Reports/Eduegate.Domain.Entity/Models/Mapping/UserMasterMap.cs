using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserMasterMap : EntityTypeConfiguration<UserMaster>
    {
        public UserMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.LoginID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.LoginPwd)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UserEmail)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Dept)
                .HasMaxLength(2);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("UserMaster");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.LoginPwd).HasColumnName("LoginPwd");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.UserEmail).HasColumnName("UserEmail");
            this.Property(t => t.Superuser).HasColumnName("Superuser");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Dept).HasColumnName("Dept");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.IsDriver).HasColumnName("IsDriver");
            this.Property(t => t.IsVendor).HasColumnName("IsVendor");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.IsRestricted).HasColumnName("IsRestricted");
        }
    }
}
