using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VehicleServiceRecordMap : EntityTypeConfiguration<VehicleServiceRecord>
    {
        public VehicleServiceRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.VehicleServiceRecordID);

            // Properties
            this.Property(t => t.Services)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("VehicleServiceRecord");
            this.Property(t => t.VehicleServiceRecordID).HasColumnName("VehicleServiceRecordID");
            this.Property(t => t.RefVehicleMasterID).HasColumnName("RefVehicleMasterID");
            this.Property(t => t.Services).HasColumnName("Services");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
