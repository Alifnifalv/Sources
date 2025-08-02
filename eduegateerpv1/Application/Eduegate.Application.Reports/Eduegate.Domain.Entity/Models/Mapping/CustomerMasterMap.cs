using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerMasterMap : EntityTypeConfiguration<CustomerMaster>
    {
        public CustomerMasterMap()
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

            this.Property(t => t.ConfirmationIP)
                .HasMaxLength(50);

            this.Property(t => t.ConfirmationCountry)
                .HasMaxLength(50);

            this.Property(t => t.Block)
                .HasMaxLength(50);

            this.Property(t => t.Street)
                .HasMaxLength(50);

            this.Property(t => t.BuildingNo)
                .HasMaxLength(50);

            this.Property(t => t.Floor)
                .HasMaxLength(20);

            this.Property(t => t.Flat)
                .HasMaxLength(20);

            this.Property(t => t.HowKnowText)
                .HasMaxLength(100);

            this.Property(t => t.CountryCode)
                .HasMaxLength(3);

            this.Property(t => t.CountryPhoneCode)
                .HasMaxLength(5);

            this.Property(t => t.MobilePin)
                .HasMaxLength(8);

            this.Property(t => t.NewsletterCategory)
                .HasMaxLength(1);

            this.Property(t => t.DigitalProductClass)
                .HasMaxLength(1);

            this.Property(t => t.CustomerLang)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Jadda)
                .HasMaxLength(10);

            this.Property(t => t.Telephone2)
                .HasMaxLength(20);

            //this.Property(t => t.RegisteredFrom)
            //    .IsFixedLength()
            //    .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("CustomerMaster");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.CustomerKey).HasColumnName("CustomerKey");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Promotion).HasColumnName("Promotion");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.RegisteredOn).HasColumnName("RegisteredOn");
            this.Property(t => t.ConfirmedOn).HasColumnName("ConfirmedOn");
            this.Property(t => t.RegistrationIP).HasColumnName("RegistrationIP");
            this.Property(t => t.RegistrationCountry).HasColumnName("RegistrationCountry");
            this.Property(t => t.ConfirmationIP).HasColumnName("ConfirmationIP");
            this.Property(t => t.ConfirmationCountry).HasColumnName("ConfirmationCountry");
            this.Property(t => t.EmailSend).HasColumnName("EmailSend");
            this.Property(t => t.EmailBounced).HasColumnName("EmailBounced");
            this.Property(t => t.Block).HasColumnName("Block");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.BuildingNo).HasColumnName("BuildingNo");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.RefAreaID).HasColumnName("RefAreaID");
            this.Property(t => t.TotalLoyaltyPoints).HasColumnName("TotalLoyaltyPoints");
            this.Property(t => t.CategorizationPoints).HasColumnName("CategorizationPoints");
            this.Property(t => t.Floor).HasColumnName("Floor");
            this.Property(t => t.Flat).HasColumnName("Flat");
            this.Property(t => t.HowKnowOption).HasColumnName("HowKnowOption");
            this.Property(t => t.HowKnowText).HasColumnName("HowKnowText");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.CountryPhoneCode).HasColumnName("CountryPhoneCode");
            this.Property(t => t.MobilePinRequired).HasColumnName("MobilePinRequired");
            this.Property(t => t.MobilePin).HasColumnName("MobilePin");
            this.Property(t => t.MobilePinSendCount).HasColumnName("MobilePinSendCount");
            this.Property(t => t.VerifiedBuyer).HasColumnName("VerifiedBuyer");
            this.Property(t => t.NewsletterCategory).HasColumnName("NewsletterCategory");
            this.Property(t => t.DigitalProductClass).HasColumnName("DigitalProductClass");
            this.Property(t => t.CustomerLang).HasColumnName("CustomerLang");
            this.Property(t => t.Jadda).HasColumnName("Jadda");
            this.Property(t => t.Telephone2).HasColumnName("Telephone2");
            //this.Property(t => t.RegisteredFrom).HasColumnName("RegisteredFrom");
        }
    }
}
