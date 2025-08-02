using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerSellUsedMap : EntityTypeConfiguration<CustomerSellUsed>
    {
        public CustomerSellUsedMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketID);

            // Properties
            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.ProductDetails)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(1000);

            this.Property(t => t.RegistrationIP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.RegistrationCountry)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("CustomerSellUsed");
            this.Property(t => t.TicketID).HasColumnName("TicketID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.ProductDetails).HasColumnName("ProductDetails");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.RegistrationIP).HasColumnName("RegistrationIP");
            this.Property(t => t.RegistrationCountry).HasColumnName("RegistrationCountry");
            this.Property(t => t.submitdt).HasColumnName("submitdt");
            this.Property(t => t.isRead).HasColumnName("isRead");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusBy).HasColumnName("StatusBy");
            this.Property(t => t.StatusDt).HasColumnName("StatusDt");
        }
    }
}
