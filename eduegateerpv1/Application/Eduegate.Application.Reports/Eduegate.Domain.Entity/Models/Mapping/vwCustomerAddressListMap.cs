using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwCustomerAddressListMap : EntityTypeConfiguration<vwCustomerAddressList>
    {
        public vwCustomerAddressListMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AddressID, t.RefCustomerID, t.RefCountryID, t.RefAreaID, t.AddressDefault, t.CountryCode, t.CountryNameEn, t.CountryActive, t.CountryNameAr, t.Jadda, t.Telephone2 });

            // Properties
            this.Property(t => t.AddressID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefCustomerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.RefCountryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefAreaID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Block)
                .HasMaxLength(50);

            this.Property(t => t.Street)
                .HasMaxLength(50);

            this.Property(t => t.BuildingNo)
                .HasMaxLength(50);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            this.Property(t => t.OtherDetails)
                .HasMaxLength(300);

            this.Property(t => t.Floor)
                .HasMaxLength(20);

            this.Property(t => t.Flat)
                .HasMaxLength(20);

            this.Property(t => t.CountryCode)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.CountryNameEn)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.AreaNameEn)
                .HasMaxLength(255);

            this.Property(t => t.GroupType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CountryNameAr)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.AreaNameAr)
                .HasMaxLength(255);

            this.Property(t => t.Jadda)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Telephone2)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("vwCustomerAddressList");
            this.Property(t => t.AddressID).HasColumnName("AddressID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.RefAreaID).HasColumnName("RefAreaID");
            this.Property(t => t.Block).HasColumnName("Block");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.BuildingNo).HasColumnName("BuildingNo");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.OtherDetails).HasColumnName("OtherDetails");
            this.Property(t => t.AddressDefault).HasColumnName("AddressDefault");
            this.Property(t => t.Floor).HasColumnName("Floor");
            this.Property(t => t.Flat).HasColumnName("Flat");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.CountryNameEn).HasColumnName("CountryNameEn");
            this.Property(t => t.CountryActive).HasColumnName("CountryActive");
            this.Property(t => t.AreaID).HasColumnName("AreaID");
            this.Property(t => t.AreaNameEn).HasColumnName("AreaNameEn");
            this.Property(t => t.AreaActive).HasColumnName("AreaActive");
            this.Property(t => t.GroupType).HasColumnName("GroupType");
            this.Property(t => t.CountryNameAr).HasColumnName("CountryNameAr");
            this.Property(t => t.AreaNameAr).HasColumnName("AreaNameAr");
            this.Property(t => t.Jadda).HasColumnName("Jadda");
            this.Property(t => t.Telephone2).HasColumnName("Telephone2");
        }
    }
}
