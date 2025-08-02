using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AddressMasterMap : EntityTypeConfiguration<AddressMaster>
    {
        public AddressMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.AddressID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

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

            this.Property(t => t.Jadda)
                .HasMaxLength(10);

            this.Property(t => t.Telephone2)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("AddressMaster");
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
            this.Property(t => t.Jadda).HasColumnName("Jadda");
            this.Property(t => t.Telephone2).HasColumnName("Telephone2");

            // Relationships
            this.HasRequired(t => t.CountryMaster)
                .WithMany(t => t.AddressMasters)
                .HasForeignKey(d => d.RefCountryID);
            this.HasRequired(t => t.CustomerMaster)
                .WithMany(t => t.AddressMasters)
                .HasForeignKey(d => d.RefCustomerID);

        }
    }
}
