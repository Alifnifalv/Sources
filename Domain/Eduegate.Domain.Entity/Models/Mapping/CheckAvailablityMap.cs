using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CheckAvailablityMap : EntityTypeConfiguration<CheckAvailablity>
    {
        public CheckAvailablityMap()
        {
            // Primary Key
            this.HasKey(t => t.CheckAvailableID);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TelephoneNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.RegistrationIP)
                .HasMaxLength(50);

            this.Property(t => t.RegistrationCountry)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CheckAvailablity");
            this.Property(t => t.CheckAvailableID).HasColumnName("CheckAvailableID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.TelephoneNo).HasColumnName("TelephoneNo");
            this.Property(t => t.RegistrationIP).HasColumnName("RegistrationIP");
            this.Property(t => t.RegistrationCountry).HasColumnName("RegistrationCountry");
            this.Property(t => t.CreatedDatetimeStamp).HasColumnName("CreatedDatetimeStamp");
        }
    }
}
