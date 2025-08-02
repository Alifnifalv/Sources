using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VehicleTypeMap : EntityTypeConfiguration<VehicleType>
    {
        public VehicleTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.VehicleTypeID);

            // Properties
            this.Property(t => t.VehicleTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VehicleTypeName)
                .HasMaxLength(50);

            this.Property(t => t.Capacity)
                .HasMaxLength(50);

            this.Property(t => t.Dimensions)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("VehicleTypes", "mutual");
            this.Property(t => t.VehicleTypeID).HasColumnName("VehicleTypeID");
            this.Property(t => t.VehicleTypeName).HasColumnName("VehicleTypeName");
            this.Property(t => t.Capacity).HasColumnName("Capacity");
            this.Property(t => t.Dimensions).HasColumnName("Dimensions");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
