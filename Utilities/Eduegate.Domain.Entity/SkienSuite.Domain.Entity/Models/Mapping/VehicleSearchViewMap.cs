using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VehicleSearchViewMap : EntityTypeConfiguration<VehicleSearchView>
    {
        public VehicleSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.VehicleIID);

            // Properties
            this.Property(t => t.VehicleCode)
                .HasMaxLength(20);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.RegistrationNo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VehicleSearchView", "mutual");
            this.Property(t => t.VehicleIID).HasColumnName("VehicleIID");
            this.Property(t => t.VehicleCode).HasColumnName("VehicleCode");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.RegistrationNo).HasColumnName("RegistrationNo");
            this.Property(t => t.PurchaseDate).HasColumnName("PurchaseDate");
            this.Property(t => t.RegistrationExpire).HasColumnName("RegistrationExpire");
            this.Property(t => t.InsuranceExpire).HasColumnName("InsuranceExpire");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
