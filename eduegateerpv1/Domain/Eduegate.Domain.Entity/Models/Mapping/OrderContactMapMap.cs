using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderContactMapMap : EntityTypeConfiguration<OrderContactMap>
    {
        public OrderContactMapMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderContactMapIID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(255);

            this.Property(t => t.MiddleName)
                .HasMaxLength(255);

            this.Property(t => t.LastName)
                .HasMaxLength(255);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            this.Property(t => t.Avenue)
                .HasMaxLength(20);

            this.Property(t => t.BuildingNo)
                .HasMaxLength(50);

            this.Property(t => t.Floor)
                .HasMaxLength(20);

            this.Property(t => t.Flat)
                .HasMaxLength(20);

            this.Property(t => t.Block)
                .HasMaxLength(50);

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

            this.Property(t => t.District)
                .HasMaxLength(200);

            this.Property(t => t.LandMark)
                .HasMaxLength(200);

            this.Property(t => t.PostalCode)
                .HasMaxLength(100);

            this.Property(t => t.Street)
                .HasMaxLength(50);

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
                .HasMaxLength(50);

            this.Property(t => t.AlternateEmailID2)
                .HasMaxLength(50);

            this.Property(t => t.WebsiteURL1)
                .HasMaxLength(100);

            this.Property(t => t.WebsiteURL2)
                .HasMaxLength(100);

            this.Property(t => t.SpecialInstruction)
                .HasMaxLength(250);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("OrderContactMaps", "orders");
            this.Property(t => t.OrderContactMapIID).HasColumnName("OrderContactMapIID");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.TitleID).HasColumnName("TitleID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.AreaID).HasColumnName("AreaID");
            this.Property(t => t.Avenue).HasColumnName("Avenue");
            this.Property(t => t.BuildingNo).HasColumnName("BuildingNo");
            this.Property(t => t.Floor).HasColumnName("Floor");
            this.Property(t => t.Flat).HasColumnName("Flat");
            this.Property(t => t.Block).HasColumnName("Block");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.AddressLine1).HasColumnName("AddressLine1");
            this.Property(t => t.AddressLine2).HasColumnName("AddressLine2");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.District).HasColumnName("District");
            this.Property(t => t.LandMark).HasColumnName("LandMark");
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
            this.Property(t => t.SpecialInstruction).HasColumnName("SpecialInstruction");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
           // this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CityID).HasColumnName("CityID");

            // Relationships
            this.HasOptional(t => t.TransactionHead)
                .WithMany(t => t.OrderContactMaps)
                .HasForeignKey(d => d.OrderID);
            this.HasOptional(t => t.Area)
                .WithMany(t => t.OrderContactMaps)
                .HasForeignKey(d => d.AreaID);
            this.HasOptional(t => t.Title)
                .WithMany(t => t.OrderContactMaps)
                .HasForeignKey(d => d.TitleID);

        }
    }
}
