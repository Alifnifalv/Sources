using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VehicleAccidentHistoryMap : EntityTypeConfiguration<VehicleAccidentHistory>
    {
        public VehicleAccidentHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.VehicleAccidentHistoryID);

            // Properties
            this.Property(t => t.Accidents)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("VehicleAccidentHistory");
            this.Property(t => t.VehicleAccidentHistoryID).HasColumnName("VehicleAccidentHistoryID");
            this.Property(t => t.RefVehicleMasterID).HasColumnName("RefVehicleMasterID");
            this.Property(t => t.Accidents).HasColumnName("Accidents");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
