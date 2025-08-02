using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VehicleMasterMap : EntityTypeConfiguration<VehicleMaster>
    {
        public VehicleMasterMap()
        {
            // Primary Key
            this.HasKey(t => new { t.VehicleMasterID, t.VehicleNo, t.Brand, t.DatePurchased, t.Registration, t.CreatedBy });

            // Properties
            this.Property(t => t.VehicleMasterID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.VehicleNo)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Brand)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.OwnershipHistory)
                .HasMaxLength(200);

            this.Property(t => t.Registration)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InsuranceType)
                .HasMaxLength(50);

            this.Property(t => t.Warranty)
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VehicleMaster");
            this.Property(t => t.VehicleMasterID).HasColumnName("VehicleMasterID");
            this.Property(t => t.VehicleNo).HasColumnName("VehicleNo");
            this.Property(t => t.Brand).HasColumnName("Brand");
            this.Property(t => t.DatePurchased).HasColumnName("DatePurchased");
            this.Property(t => t.OwnershipHistory).HasColumnName("OwnershipHistory");
            this.Property(t => t.Registration).HasColumnName("Registration");
            this.Property(t => t.OdometerReading).HasColumnName("OdometerReading");
            this.Property(t => t.InsuranceType).HasColumnName("InsuranceType");
            this.Property(t => t.DateOfExpiry).HasColumnName("DateOfExpiry");
            this.Property(t => t.Mileage).HasColumnName("Mileage");
            this.Property(t => t.Warranty).HasColumnName("Warranty");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedTimeStamp).HasColumnName("CreatedTimeStamp");
            this.Property(t => t.EditedBy).HasColumnName("EditedBy");
            this.Property(t => t.EditedTimeStanp).HasColumnName("EditedTimeStanp");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
