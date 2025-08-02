using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VehicleMap : EntityTypeConfiguration<Vehicle>
    {
        public VehicleMap()
        {
            // Primary Key
            this.HasKey(t => t.VehicleIID);

            // Properties
            this.Property(t => t.RegistrationName)
                .HasMaxLength(50);

            this.Property(t => t.VehicleCode)
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.RegistrationNo)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Vehicles", "mutual");
            this.Property(t => t.VehicleIID).HasColumnName("VehicleIID");
            this.Property(t => t.VehicleTypeID).HasColumnName("VehicleTypeID");
            this.Property(t => t.VehicleOwnershipTypeID).HasColumnName("VehicleOwnershipTypeID");
            this.Property(t => t.RegistrationName).HasColumnName("RegistrationName");
            this.Property(t => t.VehicleCode).HasColumnName("VehicleCode");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.RegistrationNo).HasColumnName("RegistrationNo");
            this.Property(t => t.PurchaseDate).HasColumnName("PurchaseDate");
            this.Property(t => t.RegistrationExpire).HasColumnName("RegistrationExpire");
            this.Property(t => t.InsuranceExpire).HasColumnName("InsuranceExpire");
            this.Property(t => t.RigistrationCityID).HasColumnName("RigistrationCityID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.RigistrationCountryID).HasColumnName("RigistrationCountryID");

            // Relationships
            this.HasOptional(t => t.City)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.RigistrationCityID);
            this.HasOptional(t => t.Country)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.RigistrationCountryID);
            this.HasOptional(t => t.VehicleOwnershipType)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.VehicleOwnershipTypeID);
            this.HasOptional(t => t.VehicleType)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(d => d.VehicleTypeID);

        }
    }
}
