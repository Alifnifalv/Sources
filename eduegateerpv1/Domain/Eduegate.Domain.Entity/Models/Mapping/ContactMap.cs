using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ContactMap : EntityTypeConfiguration<Contact>
    {
        public ContactMap()
        {
            // Primary Key
            this.HasKey(t => t.ContactIID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(255);

            this.Property(t => t.MiddleName)
                .HasMaxLength(255);

            this.Property(t => t.LastName)
                .HasMaxLength(255);

            this.Property(t => t.Description)
                .HasMaxLength(75);

            this.Property(t => t.BuildingNo)
                .HasMaxLength(75);

            this.Property(t => t.Floor)
                .HasMaxLength(75);

            this.Property(t => t.Flat)
                .HasMaxLength(75);

            this.Property(t => t.Block)
                .HasMaxLength(75);

            this.Property(t => t.AddressName)
                .HasMaxLength(500);

            this.Property(t => t.AddressLine1)
                .HasMaxLength(500);

            this.Property(t => t.AddressLine2)
                .HasMaxLength(500);

            this.Property(t => t.State)
                .HasMaxLength(100);

            this.Property(t => t.City)
                .HasMaxLength(100);

            this.Property(t => t.PostalCode)
                .HasMaxLength(100);

            this.Property(t => t.Street)
                .HasMaxLength(75);

            this.Property(t => t.TelephoneCode)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(50);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(20);

            this.Property(t => t.PhoneNo1)
                .HasMaxLength(50);

            this.Property(t => t.PhoneNo2)
                .HasMaxLength(50);

            this.Property(t => t.PassportNumber)
                .HasMaxLength(100);

            this.Property(t => t.CivilIDNumber)
                .HasMaxLength(100);

            this.Property(t => t.AlternateEmailID1)
                .HasMaxLength(100);

            this.Property(t => t.AlternateEmailID2)
                .HasMaxLength(100);

            this.Property(t => t.WebsiteURL1)
                .HasMaxLength(100);

            this.Property(t => t.WebsiteURL2)
                .HasMaxLength(100);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            this.Property(t => t.Avenue)
                .HasMaxLength(75);

            // Table & Column Mappings
            this.ToTable("Contacts", "mutual");
            this.Property(t => t.ContactIID).HasColumnName("ContactIID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.TitleID).HasColumnName("TitleID");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.BuildingNo).HasColumnName("BuildingNo");
            this.Property(t => t.Floor).HasColumnName("Floor");
            this.Property(t => t.Flat).HasColumnName("Flat");
            this.Property(t => t.Block).HasColumnName("Block");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.AddressLine1).HasColumnName("AddressLine1");
            this.Property(t => t.AddressLine2).HasColumnName("AddressLine2");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.TelephoneCode).HasColumnName("TelephoneCode");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.PhoneNo1).HasColumnName("PhoneNo1");
            this.Property(t => t.PhoneNo2).HasColumnName("PhoneNo2");
            this.Property(t => t.PassportNumber).HasColumnName("PassportNumber");
            this.Property(t => t.CivilIDNumber).HasColumnName("CivilIDNumber");
            this.Property(t => t.PassportIssueCountryID).HasColumnName("PassportIssueCountryID");
            this.Property(t => t.AlternateEmailID1).HasColumnName("AlternateEmailID1");
            this.Property(t => t.AlternateEmailID2).HasColumnName("AlternateEmailID2");
            this.Property(t => t.WebsiteURL1).HasColumnName("WebsiteURL1");
            this.Property(t => t.WebsiteURL2).HasColumnName("WebsiteURL2");
            this.Property(t => t.IsBillingAddress).HasColumnName("IsBillingAddress");
            this.Property(t => t.IsShippingAddress).HasColumnName("IsShippingAddress");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.AreaID).HasColumnName("AreaID");
            this.Property(t => t.Avenue).HasColumnName("Avenue");
            this.Property(t => t.StatusID).HasColumnName("StatusID");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.LoginID);
            this.HasOptional(t => t.Area)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.AreaID);
            this.HasOptional(t => t.Customer)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.CustomerID);
            this.HasOptional(t => t.Supplier)
                .WithMany(t => t.Contacts)
                .HasForeignKey(d => d.SupplierID);

        }
    }
}
