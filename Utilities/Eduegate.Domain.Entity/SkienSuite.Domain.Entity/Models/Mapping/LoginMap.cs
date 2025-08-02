using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LoginMap : EntityTypeConfiguration<Login>
    {
        public LoginMap()
        {
            // Primary Key
            this.HasKey(t => t.LoginIID);

            // Properties
            this.Property(t => t.LoginUserID)
                .HasMaxLength(100);

            this.Property(t => t.LoginEmailID)
                .HasMaxLength(100);

            this.Property(t => t.PasswordHint)
                .HasMaxLength(50);

            this.Property(t => t.PasswordSalt)
                .HasMaxLength(128);

            this.Property(t => t.Password)
                .HasMaxLength(128);

            this.Property(t => t.RegisteredIP)
                .HasMaxLength(20);

            this.Property(t => t.RegisteredIPCountry)
                .HasMaxLength(20);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Logins", "admin");
            this.Property(t => t.LoginIID).HasColumnName("LoginIID");
            this.Property(t => t.LoginUserID).HasColumnName("LoginUserID");
            this.Property(t => t.LoginEmailID).HasColumnName("LoginEmailID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.PasswordHint).HasColumnName("PasswordHint");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.RegisteredCountryID).HasColumnName("RegisteredCountryID");
            this.Property(t => t.RegisteredIP).HasColumnName("RegisteredIP");
            this.Property(t => t.RegisteredIPCountry).HasColumnName("RegisteredIPCountry");
            this.Property(t => t.LastLoginDate).HasColumnName("LastLoginDate");
            this.Property(t => t.RequirePasswordReset).HasColumnName("RequirePasswordReset");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Logins)
                .HasForeignKey(d => d.RegisteredCountryID);

        }
    }
}
