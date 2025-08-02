using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerMasterNumberInvalidMap : EntityTypeConfiguration<CustomerMasterNumberInvalid>
    {
        public CustomerMasterNumberInvalidMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerID);

            // Properties
            this.Property(t => t.CustomerKey)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.RegistrationIP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RegistrationCountry)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.HowKnowText)
                .HasMaxLength(100);

            this.Property(t => t.CountryCode)
                .HasMaxLength(2);

            this.Property(t => t.CountryPhoneCode)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CustomerMasterNumberInvalid");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CustomerKey).HasColumnName("CustomerKey");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Promotion).HasColumnName("Promotion");
            this.Property(t => t.RegisteredOn).HasColumnName("RegisteredOn");
            this.Property(t => t.RegistrationIP).HasColumnName("RegistrationIP");
            this.Property(t => t.RegistrationCountry).HasColumnName("RegistrationCountry");
            this.Property(t => t.HowKnowOption).HasColumnName("HowKnowOption");
            this.Property(t => t.HowKnowText).HasColumnName("HowKnowText");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.CountryPhoneCode).HasColumnName("CountryPhoneCode");
        }
    }
}
