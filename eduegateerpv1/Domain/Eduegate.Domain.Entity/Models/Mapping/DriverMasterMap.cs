using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DriverMasterMap : EntityTypeConfiguration<DriverMaster>
    {
        public DriverMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.DriverMasterID);

            // Properties
            this.Property(t => t.EmployeeNo)
                .HasMaxLength(30);

            this.Property(t => t.DriverCode)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.DriverName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PassportNo)
                .HasMaxLength(30);

            this.Property(t => t.DriverAddress)
                .HasMaxLength(500);

            this.Property(t => t.ResidensyNo)
                .HasMaxLength(30);

            this.Property(t => t.MobileNo)
                .HasMaxLength(200);

            this.Property(t => t.LicenseNo)
                .HasMaxLength(100);

            this.Property(t => t.Education)
                .HasMaxLength(200);

            this.Property(t => t.ViolationHistory)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("DriverMaster");
            this.Property(t => t.DriverMasterID).HasColumnName("DriverMasterID");
            this.Property(t => t.EmployeeNo).HasColumnName("EmployeeNo");
            this.Property(t => t.DriverCode).HasColumnName("DriverCode");
            this.Property(t => t.DriverName).HasColumnName("DriverName");
            this.Property(t => t.DateofJoin).HasColumnName("DateofJoin");
            this.Property(t => t.DateofBirth).HasColumnName("DateofBirth");
            this.Property(t => t.PassportNo).HasColumnName("PassportNo");
            this.Property(t => t.PassportIssueDate).HasColumnName("PassportIssueDate");
            this.Property(t => t.PassoprtExpiryDate).HasColumnName("PassoprtExpiryDate");
            this.Property(t => t.DriverAddress).HasColumnName("DriverAddress");
            this.Property(t => t.ResidensyType).HasColumnName("ResidensyType");
            this.Property(t => t.ResidensyNo).HasColumnName("ResidensyNo");
            this.Property(t => t.ResidensyStatus).HasColumnName("ResidensyStatus");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.LicenseNo).HasColumnName("LicenseNo");
            this.Property(t => t.LicenseIssueDate).HasColumnName("LicenseIssueDate");
            this.Property(t => t.LicenseExpiryDate).HasColumnName("LicenseExpiryDate");
            this.Property(t => t.Education).HasColumnName("Education");
            this.Property(t => t.LicenseType).HasColumnName("LicenseType");
            this.Property(t => t.BloodGroup).HasColumnName("BloodGroup");
            this.Property(t => t.Nationality).HasColumnName("Nationality");
            this.Property(t => t.ViolationHistory).HasColumnName("ViolationHistory");
            this.Property(t => t.CreatedDatetimeStamp).HasColumnName("CreatedDatetimeStamp");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.EditedDatetimeStamp).HasColumnName("EditedDatetimeStamp");
            this.Property(t => t.EditedByID).HasColumnName("EditedByID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
