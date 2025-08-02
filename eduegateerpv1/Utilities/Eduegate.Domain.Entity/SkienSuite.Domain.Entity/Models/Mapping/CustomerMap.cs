using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerIID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(255);

            this.Property(t => t.MiddleName)
                .HasMaxLength(255);

            this.Property(t => t.LastName)
                .HasMaxLength(255);

            this.Property(t => t.CivilIDNumber)
                .HasMaxLength(100);

            this.Property(t => t.CustomerCR)
                .HasMaxLength(50);

            this.Property(t => t.PassportNumber)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.HowKnowText)
                .HasMaxLength(200);

            this.Property(t => t.Telephone)
                .HasMaxLength(20);

            this.Property(t => t.CustomerCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Customers", "mutual");
            this.Property(t => t.CustomerIID).HasColumnName("CustomerIID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.TitleID).HasColumnName("TitleID");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.IsOfflineCustomer).HasColumnName("IsOfflineCustomer");
            this.Property(t => t.IsDifferentBillingAddress).HasColumnName("IsDifferentBillingAddress");
            this.Property(t => t.IsTermsAndConditions).HasColumnName("IsTermsAndConditions");
            this.Property(t => t.IsSubscribeForNewsLetter).HasColumnName("IsSubscribeForNewsLetter");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CivilIDNumber).HasColumnName("CivilIDNumber");
            this.Property(t => t.PassportIssueCountryID).HasColumnName("PassportIssueCountryID");
            this.Property(t => t.CustomerCR).HasColumnName("CustomerCR");
            this.Property(t => t.CRExpiryDate).HasColumnName("CRExpiryDate");
            this.Property(t => t.PassportNumber).HasColumnName("PassportNumber");
            this.Property(t => t.ParentCustomerID).HasColumnName("ParentCustomerID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.HowKnowOptionID).HasColumnName("HowKnowOptionID");
            this.Property(t => t.HowKnowText).HasColumnName("HowKnowText");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.ProductManagerID).HasColumnName("ProductManagerID");
            this.Property(t => t.IsMigrated).HasColumnName("IsMigrated");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.CustomerCode).HasColumnName("CustomerCode");
        }
    }
}
